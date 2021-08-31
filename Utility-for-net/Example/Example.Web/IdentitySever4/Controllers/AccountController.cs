////#define Identity4_0
//#define Identity3_0
//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Threading.Tasks;
//using IdentityServer4.Events;
//using IdentityServer4.Extensions;
//using Microsoft.AspNetCore.Authentication;
//using Microsoft.AspNetCore.Mvc;
//using IdentityModel;
//using IdentityServer4;
//using IdentityServer4.Models;
//using IdentityServer4.Services;
//using IdentityServer4.Stores;
//using IdentityServer4.Test;
//using Microsoft.AspNetCore.Http;
//using Microsoft.Extensions.Logging;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.Extensions.Hosting;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Identity;
//using IdentityServerHost.Quickstart.UI;
//using IdentityServer.Models;
//using Utility.IdentityServer4;

//namespace Utility.IdentityServer41
//{
//    [SecurityHeaders]
//    [Authorize]
//    //location account 改成其他重定向失败 框架写死了 不知道到哪里改 先不管了
//    public class AccountController : Controller
//    {
//        private readonly TestUserStore _users;//基于内存用户
//        //基于数据库实现用户
//        private readonly UserManager<ApplicationUser> _userManager;
//        private readonly SignInManager<ApplicationUser> _signInManager;

//        private readonly IIdentityServerInteractionService _interaction;
//        private readonly IClientStore _clientStore;
//        private readonly IResourceStore _resourceStore;
//        private readonly IAuthenticationSchemeProvider _schemeProvider;
//        private readonly IDeviceFlowInteractionService _deviceFlowInteractionService;
//        private readonly IEventService _events;
//        private readonly IWebHostEnvironment _environment;
//        private readonly ILogger<AccountController> _logger;

//        public AccountController(
//            IIdentityServerInteractionService interaction,
//            IClientStore clientStore,
//            IResourceStore resourceStore,
//            IAuthenticationSchemeProvider schemeProvider,
//            IEventService events,
//            IDeviceFlowInteractionService deviceFlowInteractionService,
//            IWebHostEnvironment environment, ILogger<AccountController> logger,
//            TestUserStore users = null, UserManager<ApplicationUser> userManager=null,
//            SignInManager<ApplicationUser> signInManager=null)
//        {
//            // if the TestUserStore is not in DI, then we'll just use the global users collection
//            // this is where you would plug in your own custom identity management library (e.g. ASP.NET Identity)
//            _users = users ?? new TestUserStore(TestUsers.Users);

//            _interaction = interaction;
//            _clientStore = clientStore;
//            this._resourceStore = resourceStore;
//            _environment = environment;
//            _schemeProvider = schemeProvider;
//            _events = events;
//            _deviceFlowInteractionService = deviceFlowInteractionService;
//            _logger = logger;
//            _userManager = userManager;
//            _signInManager = signInManager;
//        }
//        [AllowAnonymous]
//        public IActionResult Index()
//        {
//            if (_environment.IsDevelopment())
//            {
//                // only show in development
//                return View();
//            }

//            _logger.LogInformation("Homepage is disabled in production. Returning 404.");
//            return NotFound();
//        }
//        /// <summary>
//        /// Shows the error page
//        /// </summary>
//        [AllowAnonymous]
//        public async Task<IActionResult> Error(string errorId)
//        {
//            var vm = new ErrorViewModel();

//            // retrieve error details from identityserver
//            var message = await _interaction.GetErrorContextAsync(errorId);
//            if (message != null)
//            {
//                vm.Error = message;

//                if (!_environment.IsDevelopment())
//                {
//                    // only show in development
//                    message.ErrorDescription = null;
//                }
//            }

//            return View("Error", vm);
//        }

//        /*此示例控制器为本地和外部帐户实现典型的登录/注销/设置工作流。登录服务封装了与用户数据存储的交互。此数据存储仅在内存中，不能用于生产！
//        交互服务为UI提供了一种与identityserver通信的方式，用于验证和上下文检索*/
//        #region account

//        /// <summary>
//        /// Entry point into the login workflow
//        /// </summary>
//        [HttpGet]
//        [AllowAnonymous]
//        public async Task<IActionResult> Login(string returnUrl)
//        {
//            // build a model so we know what to show on the login page
//            var vm = await BuildLoginViewModelAsync(returnUrl);

//            if (vm.IsExternalLoginOnly)
//            {
//                // we only have one option for logging in and it's an external provider
//                return RedirectToAction("Challenge", "External", new { provider = vm.ExternalLoginScheme, returnUrl });
//            }

//            return View(vm);
//        }
//        [HttpGet]
//        [AllowAnonymous]
//        public IActionResult Register(string returnUrl)
//        {
//            return View();
//        }

//        /// <summary>
//        /// Handle postback from username/password login
//        /// </summary>
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        [AllowAnonymous]
//        public async Task<IActionResult> Login(LoginInputModel model, string button)
//        {
//            // check if we are in the context of an authorization request
//            var context = await _interaction.GetAuthorizationContextAsync(model.ReturnUrl);

//            // the user clicked the "cancel" button
//            if (button != "login")
//            {
//                if (context != null)
//                {
//                    // if the user cancels, send a result back into IdentityServer as if they 
//                    // denied the consent (even if this client does not require consent).
//                    // this will send back an access denied OIDC error response to the client.
//                    await _interaction.GrantConsentAsync(context,new ConsentResponse());

//                    // we can trust model.ReturnUrl since GetAuthorizationContextAsync returned non-null
//#if Identity3_0
//                    if (await _clientStore.IsPkceClientAsync(context.ClientId))
//#else
//                    if (await _clientStore.IsPkceClientAsync(context.Client.ClientId))
//#endif
//                    {
//                        //如果客户机是PKCE，那么我们假设它是本机的，因此在如何返回响应是为了更好地为最终用户提供用户体验。
//                        return this.LoadingPage("Redirect", model.ReturnUrl);
//                    }

//                    return Redirect(model.ReturnUrl);
//                }
//                else
//                {
//                    // 因为我们没有有效的上下文，所以我们返回主页
//                    return Redirect("~/");
//                }
//            }

//            if (ModelState.IsValid)
//            {
//                var result = false;
//                // 根据数据库存储验证用户名/密码
//                if (_signInManager!=null)
//                {
//                    result =  _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberLogin, lockoutOnFailure: true).Result.Succeeded;
//                }
//                // 根据内存存储验证用户名/密码
//               else if (_users != null)
//                {
//                    result = _users.ValidateCredentials(model.Username, model.Password);
//                }
//                if (result)
//                {
//                    if (_signInManager != null)
//                    {
//                        var user = await _userManager.FindByNameAsync(model.Username );
//#if Identity3_0
//                        await _events.RaiseAsync(new UserLoginSuccessEvent(user.UserName, user.Id, user.UserName, clientId: context.ClientId));
//#else
//                        await _events.RaiseAsync(new UserLoginSuccessEvent(user.UserName, user.Id, user.UserName, clientId: context?.Client.ClientId));
//#endif
//                    }
//                    else if (_users != null)
//                    {
//                        var user = _users.FindByUsername(model.Username);
//#if Identity3_0
//                        await _events.RaiseAsync(new UserLoginSuccessEvent(user.Username, user.SubjectId, user.Username, clientId: context.ClientId));
//#else
//                        await _events.RaiseAsync(new UserLoginSuccessEvent(user.Username, user.SubjectId, user.Username, clientId: context?.Client.ClientId));
//#endif
//                        //仅当用户选择“记住我”时才在此处设置显式过期。

//                        //否则，我们依赖于cookie中间件中配置的过期时间。
//                        AuthenticationProperties props = null;
//                        if (AccountOptions.AllowRememberLogin && model.RememberLogin)
//                        {
//                            props = new AuthenticationProperties
//                            {
//                                IsPersistent = true,
//                                ExpiresUtc = DateTimeOffset.UtcNow.Add(AccountOptions.RememberMeLoginDuration)
//                            };
//                        };

//                        // issue authentication cookie with subject ID and username
//                        var isuser = new IdentityServerUser(user.SubjectId)
//                        {
//                            DisplayName = user.Username
//                        };

//                        await HttpContext.SignInAsync(isuser, props);
//                    }

//                    if (context != null)
//                    {
//#if Identity3_0
//                        if (await _clientStore.IsPkceClientAsync(context.ClientId))
//#else
//                        if (await _clientStore.IsPkceClientAsync(context?.Client.ClientId))
//#endif
//                        {
//                            // if the client is PKCE then we assume it's native, so this change in how to
//                            // return the response is for better UX for the end user.
//                            return this.LoadingPage("Redirect", model.ReturnUrl);
//                        }

//                        // we can trust model.ReturnUrl since GetAuthorizationContextAsync returned non-null
//                        return Redirect(model.ReturnUrl);
//                    }

//                    // request for a local page
//                    if (Url.IsLocalUrl(model.ReturnUrl))
//                    {
//                        return Redirect(model.ReturnUrl);
//                    }
//                    else if (string.IsNullOrEmpty(model.ReturnUrl))
//                    {
//                        return Redirect("~/");
//                    }
//                    else
//                    {
//                        // user might have clicked on a malicious link - should be logged
//                        throw new Exception("invalid return URL");
//                    }
//                }
//#if Identity3_0
//                await _events.RaiseAsync(new UserLoginFailureEvent(model.Username, "invalid credentials", clientId: context.ClientId));
//#else
//                await _events.RaiseAsync(new UserLoginFailureEvent(model.Username, "invalid credentials", clientId: context?.Client.ClientId));
//#endif
//                ModelState.AddModelError(string.Empty, AccountOptions.InvalidCredentialsErrorMessage);
//            }

//            // something went wrong, show form with error
//            var vm = await BuildLoginViewModelAsync(model);
//            return View(vm);
//        }


//        /// <summary>
//        /// Show logout page
//        /// </summary>
//        [HttpGet]
//        [AllowAnonymous]
//        public async Task<IActionResult> Logout(string logoutId)
//        {
//            // build a model so the logout page knows what to display
//            var vm = await BuildLogoutViewModelAsync(logoutId);

//            if (vm.ShowLogoutPrompt == false)
//            {
//                // if the request for logout was properly authenticated from IdentityServer, then
//                // we don't need to show the prompt and can just log the user out directly.
//                return await Logout(vm);
//            }

//            return View(vm);
//        }

//        /// <summary>
//        /// Handle logout page postback
//        /// </summary>
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        [AllowAnonymous]
//        public async Task<IActionResult> Logout(LogoutInputModel model)
//        {
//            // build a model so the logged out page knows what to display
//            var vm = await BuildLoggedOutViewModelAsync(model.LogoutId);

//            if (User?.Identity.IsAuthenticated == true)
//            {
//                // delete local authentication cookie
//                if (_signInManager != null)
//                {
//                  await  _signInManager.SignOutAsync();
//                }
//                else
//                {
//                    await HttpContext.SignOutAsync();
//                }

//                // raise the logout event
//                await _events.RaiseAsync(new UserLogoutSuccessEvent(User.GetSubjectId(), User.GetDisplayName()));
//            }

//            // check if we need to trigger sign-out at an upstream identity provider
//            if (vm.TriggerExternalSignout)
//            {
//                // build a return URL so the upstream provider will redirect back
//                // to us after the user has logged out. this allows us to then
//                // complete our single sign-out processing.
//                string url = Url.Action("Logout", new { logoutId = vm.LogoutId });

//                // this triggers a redirect to the external provider for sign-out
//                return SignOut(new AuthenticationProperties { RedirectUri = url }, vm.ExternalAuthenticationScheme);
//            }

//            return View("LoggedOut", vm);
//        }

//        [HttpGet]
//        [AllowAnonymous]
//        public IActionResult AccessDenied()
//        {
//            return View();
//        }


//        /*****************************************/
//        /* helper APIs for the AccountController */
//        /*****************************************/
//        private async Task<LoginViewModel> BuildLoginViewModelAsync(string returnUrl)
//        {
//            var context = await _interaction.GetAuthorizationContextAsync(returnUrl);
//            if (context?.IdP != null && await _schemeProvider.GetSchemeAsync(context.IdP) != null)
//            {
//                var local = context.IdP == IdentityServer4.IdentityServerConstants.LocalIdentityProvider;

//                // this is meant to short circuit the UI and only trigger the one external IdP
//                var vm = new LoginViewModel
//                {
//                    EnableLocalLogin = local,
//                    ReturnUrl = returnUrl,
//                    Username = context?.LoginHint,
//                };

//                if (!local)
//                {
//                    vm.ExternalProviders = new[] { new ExternalProvider { AuthenticationScheme = context.IdP } };
//                }

//                return vm;
//            }

//            var schemes = await _schemeProvider.GetAllSchemesAsync();

//            var providers = schemes
//                .Where(x => x.DisplayName != null ||
//                            (x.Name.Equals(AccountOptions.WindowsAuthenticationSchemeName, StringComparison.OrdinalIgnoreCase))
//                )
//                .Select(x => new ExternalProvider
//                {
//                    DisplayName = x.DisplayName ?? x.Name,
//                    AuthenticationScheme = x.Name
//                }).ToList();

//            var allowLocal = true;
//#if Identity3_0
//            if (context.ClientId != null)
//            {
//                var client = await _clientStore.FindEnabledClientByIdAsync(context.ClientId);
//#else
//            if (context?.Client.ClientId != null)
//            {
//                var client = await _clientStore.FindEnabledClientByIdAsync(context.Client.ClientId);
//#endif
//                if (client != null)
//                {
//                    allowLocal = client.EnableLocalLogin;

//                    if (client.IdentityProviderRestrictions != null && client.IdentityProviderRestrictions.Any())
//                    {
//                        providers = providers.Where(provider => client.IdentityProviderRestrictions.Contains(provider.AuthenticationScheme)).ToList();
//                    }
//                }
//            }

//            return new LoginViewModel
//            {
//                AllowRememberLogin = AccountOptions.AllowRememberLogin,
//                EnableLocalLogin = allowLocal && AccountOptions.AllowLocalLogin,
//                ReturnUrl = returnUrl,
//                Username = context?.LoginHint,
//                ExternalProviders = providers.ToArray()
//            };
//        }

//        private async Task<LoginViewModel> BuildLoginViewModelAsync(LoginInputModel model)
//        {
//            var vm = await BuildLoginViewModelAsync(model.ReturnUrl);
//            vm.Username = model.Username;
//            vm.RememberLogin = model.RememberLogin;
//            return vm;
//        }

//        private async Task<LogoutViewModel> BuildLogoutViewModelAsync(string logoutId)
//        {
//            var vm = new LogoutViewModel { LogoutId = logoutId, ShowLogoutPrompt = AccountOptions.ShowLogoutPrompt };

//            if (User?.Identity.IsAuthenticated != true)
//            {
//                // if the user is not authenticated, then just show logged out page
//                vm.ShowLogoutPrompt = false;
//                return vm;
//            }

//            var context = await _interaction.GetLogoutContextAsync(logoutId);
//            if (context?.ShowSignoutPrompt == false)
//            {
//                // it's safe to automatically sign-out
//                vm.ShowLogoutPrompt = false;
//                return vm;
//            }

//            // show the logout prompt. this prevents attacks where the user
//            // is automatically signed out by another malicious web page.
//            return vm;
//        }

//        private async Task<LoggedOutViewModel> BuildLoggedOutViewModelAsync(string logoutId)
//        {
//            // get context information (client name, post logout redirect URI and iframe for federated signout)
//            var logout = await _interaction.GetLogoutContextAsync(logoutId);

//            var vm = new LoggedOutViewModel
//            {
//                AutomaticRedirectAfterSignOut = AccountOptions.AutomaticRedirectAfterSignOut,
//                PostLogoutRedirectUri = logout?.PostLogoutRedirectUri,
//                ClientName = string.IsNullOrEmpty(logout?.ClientName) ? logout?.ClientId : logout?.ClientName,
//                SignOutIframeUrl = logout?.SignOutIFrameUrl,
//                LogoutId = logoutId
//            };

//            if (User?.Identity.IsAuthenticated == true)
//            {
//                var idp = User.FindFirst(JwtClaimTypes.IdentityProvider)?.Value;
//                if (idp != null && idp != IdentityServer4.IdentityServerConstants.LocalIdentityProvider)
//                {
//                    var providerSupportsSignout = await HttpContext.GetSchemeSupportsSignOutAsync(idp);
//                    if (providerSupportsSignout)
//                    {
//                        if (vm.LogoutId == null)
//                        {
//                            // if there's no current logout context, we need to create one
//                            // this captures necessary info from the current logged in user
//                            // before we signout and redirect away to the external IdP for signout
//                            vm.LogoutId = await _interaction.CreateLogoutContextAsync();
//                        }

//                        vm.ExternalAuthenticationScheme = idp;
//                    }
//                }
//            }

//            return vm;
//        }

//#endregion account

//#region consent 同意
//        /// <summary>
//        /// Shows the consent screen
//        /// </summary>
//        /// <param name="returnUrl"></param>
//        /// <returns></returns>
//        [HttpGet]
//        public async Task<IActionResult> Consent(string returnUrl)
//        {
//            var vm = await BuildViewModelAsync(returnUrl);
//            if (vm != null)
//            {
//                return View("Index", vm);
//            }

//            return View("Error");
//        }

//        /// <summary>
//        /// Handles the consent screen postback
//        /// </summary>
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Consent(ConsentInputModel model)
//        {
//            var result = await ProcessConsent(model);

//            if (result.IsRedirect)
//            {
//                if (await _clientStore.IsPkceClientAsync(result.ClientId))
//                {
//                    // if the client is PKCE then we assume it's native, so this change in how to
//                    // return the response is for better UX for the end user.
//                    return this.LoadingPage("Redirect", result.RedirectUri);
//                }

//                return Redirect(result.RedirectUri);
//            }

//            if (result.HasValidationError)
//            {
//                ModelState.AddModelError(string.Empty, result.ValidationError);
//            }

//            if (result.ShowView)
//            {
//                return View("Index", result.ViewModel);
//            }

//            return View("Error");
//        }

//        /*****************************************/
//        /* helper APIs for the ConsentController */
//        /*****************************************/
//        private async Task<ProcessConsentResult> ProcessConsent(ConsentInputModel model)
//        {
//            var result = new ProcessConsentResult();

//            // validate return url is still valid
//            var request = await _interaction.GetAuthorizationContextAsync(model.ReturnUrl);
//            if (request == null) return result;

//            ConsentResponse grantedConsent = null;

//            // user clicked 'no' - send back the standard 'access_denied' response
//            if (model?.Button == "no")
//            {
//                grantedConsent =new ConsentResponse();

//                // emit event
//#if Identity3_0
//                await _events.RaiseAsync(new ConsentDeniedEvent(User.GetSubjectId(), request.ClientId, request.ScopesRequested));
//#else
//                await _events.RaiseAsync(new ConsentDeniedEvent(User.GetSubjectId(), request.Client?.ClientId, request.Client.AllowedScopes));
//#endif
//            }
//            // user clicked 'yes' - validate the data
//            else if (model?.Button == "yes")
//            {
//                // if the user consented to some scope, build the response model
//                if (model.ScopesConsented != null && model.ScopesConsented.Any())
//                {
//                    var scopes = model.ScopesConsented;
//                    if (ConsentOptions.EnableOfflineAccess == false)
//                    {
//                        scopes = scopes.Where(x => x != IdentityServer4.IdentityServerConstants.StandardScopes.OfflineAccess);
//                    }

//                    grantedConsent = new ConsentResponse
//                    {
//                        RememberConsent = model.RememberConsent,
//#if Identity3_0
//                        ScopesConsented=scopes.ToArray()
//#else
//                        ScopesValuesConsented = scopes.ToArray()
//#endif
//                    };

//                    // emit event
//#if Identity3_0
//                    await _events.RaiseAsync(new ConsentGrantedEvent(User.GetSubjectId(), request.ClientId, request.RequestObjectValues.Values, grantedConsent.ScopesConsented, grantedConsent.RememberConsent));
//#else
//                    await _events.RaiseAsync(new ConsentGrantedEvent(User.GetSubjectId(), request.Client.ClientId, request.ValidatedResources.GetRequiredScopeValues(), grantedConsent.ScopesValuesConsented, grantedConsent.RememberConsent));
//#endif
//                }
//                else
//                {
//                    result.ValidationError = ConsentOptions.MustChooseOneErrorMessage;
//                }
//            }
//            else
//            {
//                result.ValidationError = ConsentOptions.InvalidSelectionErrorMessage;
//            }

//            if (grantedConsent != null)
//            {
//                // communicate outcome of consent back to identityserver
//                await _interaction.GrantConsentAsync(request, grantedConsent);

//                // indicate that's it ok to redirect back to authorization endpoint
//                result.RedirectUri = model.ReturnUrl;
//#if Identity3_0
//                result.ClientId = request.ClientId;
//#else
//                result.ClientId = request.Client.ClientId;
//#endif
//            }
//            else
//            {
//                // we need to redisplay the consent UI
//                result.ViewModel = await BuildViewModelAsync(model.ReturnUrl, model);
//            }

//            return result;
//        }

//        private async Task<ConsentViewModel> BuildViewModelAsync(string returnUrl, ConsentInputModel model = null)
//        {
//            var request = await _interaction.GetAuthorizationContextAsync(returnUrl);
//            if (request != null)
//            {
//#if Identity3_0
//                var client = await _clientStore.FindEnabledClientByIdAsync(request.ClientId);
//#else
//                var client = await _clientStore.FindEnabledClientByIdAsync(request.Client.ClientId);
//#endif
//                if (client != null)
//                {
//#if Identity3_0
//                    var resources = await _resourceStore.FindEnabledResourcesByScopeAsync(request.RequestObjectValues.Values);
//#else
//                    var resources = await _resourceStore.FindEnabledResourcesByScopeAsync(request.ValidatedResources.GetRequiredScopeValues());
//#endif
//                    if (resources != null && (resources.IdentityResources.Any() || resources.ApiResources.Any()))
//                    {
//                        return CreateConsentViewModel(model, returnUrl, request, client, resources);
//                    }
//                    else
//                    {
//#if Identity3_0
//                        _logger.LogError("No scopes matching: {0}", request.ScopesRequested.Aggregate((x, y) => x + ", " + y));
//#else
//                        _logger.LogError("No scopes matching: {0}", request.Client.AllowedScopes.Aggregate((x, y) => x + ", " + y));
//#endif
//                    }
//                }
//                else
//                {
//#if Identity3_0
//                    _logger.LogError("Invalid client id: {0}", request.ClientId);
//#else
//                    _logger.LogError("Invalid client id: {0}", request.Client.ClientId);
//#endif
//                }
//            }
//            else
//            {
//                _logger.LogError("No consent request matching request: {0}", returnUrl);
//            }

//            return null;
//        }

//        private ConsentViewModel CreateConsentViewModel(
//            ConsentInputModel model, string returnUrl,
//            AuthorizationRequest request,
//            Client client, Resources resources)
//        {
//            var vm = new ConsentViewModel
//            {
//                RememberConsent = model?.RememberConsent ?? true,
//                ScopesConsented = model?.ScopesConsented ?? Enumerable.Empty<string>(),

//                ReturnUrl = returnUrl,

//                ClientName = client.ClientName ?? client.ClientId,
//                ClientUrl = client.ClientUri,
//                ClientLogoUrl = client.LogoUri,
//                AllowRememberConsent = client.AllowRememberConsent
//            };

//            vm.IdentityScopes = resources.IdentityResources.Select(x => CreateScopeViewModel(x, vm.ScopesConsented.Contains(x.Name) || model == null)).ToArray();
//#if Identity3_0
//            vm.ResourceScopes = resources.ApiResources.SelectMany(x => x.Scopes).Select(x => CreateScopeViewModel(x, vm.ScopesConsented.Contains(x.Name) || model == null)).ToArray();
//#else
//            vm.ResourceScopes = resources.ApiResources.SelectMany(x => x.Scopes).Select(x => CreateScopeViewModel(x, vm.ScopesConsented.Contains(x) || model == null)).ToArray();
//#endif
//            if (ConsentOptions.EnableOfflineAccess && resources.OfflineAccess)
//            {
//                vm.ResourceScopes = vm.ResourceScopes.Union(new ScopeViewModel[] {
//                    GetOfflineAccessScope(vm.ScopesConsented.Contains(IdentityServer4.IdentityServerConstants.StandardScopes.OfflineAccess) || model == null)
//                });
//            }

//            return vm;
//        }

//#endregion consent


//#region Device 设备
//        [HttpGet]
//        public async Task<IActionResult> Device([FromQuery(Name = "user_code")] string userCode)
//        {
//            if (string.IsNullOrWhiteSpace(userCode)) return View("UserCodeCapture");

//            var vm = await BuildDeviceViewModelAsync(userCode);
//            if (vm == null) return View("Error");

//            vm.ConfirmUserCode = true;
//            return View("UserCodeConfirmation", vm);
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> UserCodeCapture(string userCode)
//        {
//            var vm = await BuildViewModelAsync(userCode);
//            if (vm == null) return View("Error");

//            return View("UserCodeConfirmation", vm);
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Callback(DeviceAuthorizationInputModel model)
//        {
//            if (model == null) throw new ArgumentNullException(nameof(model));

//            var result = await ProcessConsent(model);
//            if (result.HasValidationError) return View("Error");

//            return View("Success");
//        }

//        private async Task<ProcessConsentResult> ProcessConsent(DeviceAuthorizationInputModel model)
//        {
//            var result = new ProcessConsentResult();

//            var request = await _deviceFlowInteractionService.GetAuthorizationContextAsync(model.UserCode);
//            if (request == null) return result;

//            ConsentResponse grantedConsent = null;

//            // user clicked 'no' - send back the standard 'access_denied' response
//            if (model.Button == "no")
//            {
//                grantedConsent = new ConsentResponse();

//                // emit event
//#if Identity3_0
//                await _events.RaiseAsync(new ConsentDeniedEvent(User.GetSubjectId(), request.ClientId, request.ScopesRequested));
//#else
//                await _events.RaiseAsync(new ConsentDeniedEvent(User.GetSubjectId(), request.Client.ClientId, request.ValidatedResources.GetRequiredScopeValues()));
//#endif
//            }
//            // user clicked 'yes' - validate the data
//            else if (model.Button == "yes")
//            {
//                // if the user consented to some scope, build the response model
//                if (model.ScopesConsented != null && model.ScopesConsented.Any())
//                {
//                    var scopes = model.ScopesConsented;
//                    if (ConsentOptions.EnableOfflineAccess == false)
//                    {
//                        scopes = scopes.Where(x => x != IdentityServer4.IdentityServerConstants.StandardScopes.OfflineAccess);
//                    }

//                    grantedConsent = new ConsentResponse
//                    {
//                        RememberConsent = model.RememberConsent,
//#if Identity3_0
//                        ScopesConsented=scopes.ToArray()
//#else
//                        ScopesValuesConsented = scopes.ToArray()
//#endif
//                    };

//                    // emit event
//#if Identity3_0
//                    await _events.RaiseAsync(new ConsentGrantedEvent(User.GetSubjectId(), request.ClientId, request.ScopesRequested, grantedConsent.ScopesConsented, grantedConsent.RememberConsent));
//#else
//                    await _events.RaiseAsync(new ConsentGrantedEvent(User.GetSubjectId(), request.Client.ClientId, request.ValidatedResources.GetRequiredScopeValues(), grantedConsent.ScopesValuesConsented, grantedConsent.RememberConsent));
//#endif
//                }
//                else
//                {
//                    result.ValidationError = ConsentOptions.MustChooseOneErrorMessage;
//                }
//            }
//            else
//            {
//                result.ValidationError = ConsentOptions.InvalidSelectionErrorMessage;
//            }

//            if (grantedConsent != null)
//            {
//                // communicate outcome of consent back to identityserver
//                await _deviceFlowInteractionService.HandleRequestAsync(model.UserCode, grantedConsent);

//                // indicate that's it ok to redirect back to authorization endpoint
//                result.RedirectUri = model.ReturnUrl;
//#if Identity3_0
//                result.ClientId = request.ClientId;
//#else
//                result.ClientId = request.Client.ClientId;
//#endif
//            }
//            else
//            {
//                // we need to redisplay the consent UI
//                result.ViewModel = await BuildViewModelAsync(model.UserCode, model);
//            }

//            return result;
//        }

//        private async Task<DeviceAuthorizationViewModel> BuildDeviceViewModelAsync(string userCode, DeviceAuthorizationInputModel model = null)
//        {
            

//            var request = await _deviceFlowInteractionService.GetAuthorizationContextAsync(userCode);
//            if (request != null)
//            {
//#if Identity3_0
//                var client = await _clientStore.FindEnabledClientByIdAsync(request.ClientId);
//#else
//                var client = await _clientStore.FindEnabledClientByIdAsync(request.Client.ClientId);
//#endif
//                if (client != null)
//                {
//#if Identity3_0
//                    var resources = await _resourceStore.FindEnabledResourcesByScopeAsync(request.ScopesRequested);
//#else
//                    var resources = await _resourceStore.FindEnabledResourcesByScopeAsync(request.ValidatedResources.GetRequiredScopeValues());
//#endif
//                    if (resources != null && (resources.IdentityResources.Any() || resources.ApiResources.Any()))
//                    {
//                        return CreateConsentViewModel(userCode, model, client, resources);
//                    }
//                    else
//                    {
//#if Identity3_0
//                        _logger.LogError("No scopes matching: {0}", request.ScopesRequested.Aggregate((x, y) => x + ", " + y));
//#else
//                        _logger.LogError("No scopes matching: {0}", request.ValidatedResources.GetRequiredScopeValues().Aggregate((x, y) => x + ", " + y));
//#endif
//                    }
//                }
//                else
//                {
//#if Identity3_0
//                    _logger.LogError("Invalid client id: {0}", request.ClientId);
//#else
//                    _logger.LogError("Invalid client id: {0}", request.Client.ClientId);
//#endif
//                }
//            }

//            return null;
//        }

//        private DeviceAuthorizationViewModel CreateConsentViewModel(string userCode, DeviceAuthorizationInputModel model, Client client, Resources resources)
//        {
//            var vm = new DeviceAuthorizationViewModel
//            {
//                UserCode = userCode,

//                RememberConsent = model?.RememberConsent ?? true,
//                ScopesConsented = model?.ScopesConsented ?? Enumerable.Empty<string>(),

//                ClientName = client.ClientName ?? client.ClientId,
//                ClientUrl = client.ClientUri,
//                ClientLogoUrl = client.LogoUri,
//                AllowRememberConsent = client.AllowRememberConsent
//            };
//#if Identity3_0
//            vm.IdentityScopes = resources.IdentityResources.Select(x => CreateScopeViewModel(x, vm.ScopesConsented.Contains(x.Name) || model == null)).ToArray();
//            vm.ResourceScopes = resources.ApiResources.SelectMany(x => x.Scopes).Select(x => CreateScopeViewModel(x, vm.ScopesConsented.Contains(x.Name) || model == null)).ToArray();
//#else
//            vm.IdentityScopes = resources.IdentityResources.Select(x => CreateScopeViewModel(x, vm.ScopesConsented.Contains(x.Name) || model == null)).ToArray();
//            vm.ResourceScopes = resources.ApiResources.SelectMany(x => x.Scopes).Select(x => CreateScopeViewModel(x, vm.ScopesConsented.Contains(x) || model == null)).ToArray();
//#endif
//            if (ConsentOptions.EnableOfflineAccess && resources.OfflineAccess)
//            {
//                vm.ResourceScopes = vm.ResourceScopes.Union(new[]
//                {
//                    GetOfflineAccessScope(vm.ScopesConsented.Contains(IdentityServerConstants.StandardScopes.OfflineAccess) || model == null)
//                });
//            }

//            return vm;
//        }
////#if Identity3_0
//        private ScopeViewModel CreateScopeViewModel(Scope scope, bool check)
//        {
//            return new ScopeViewModel
//            {
//                Name = scope.Name,
//                DisplayName = scope.DisplayName,
//                Description = scope.Description,
//                Emphasize = scope.Emphasize,
//                Required = scope.Required,
//                Checked = check || scope.Required
//            };
//        }
////#else
//        private ScopeViewModel CreateScopeViewModel(IdentityResource identity, bool check)
//        {
//            return new ScopeViewModel
//            {
//                Name = identity.Name,
//                DisplayName = identity.DisplayName,
//                Description = identity.Description,
//                Emphasize = identity.Emphasize,
//                Required = identity.Required,
//                Checked = check || identity.Required
//            };
//        }
////#endif
//        public ScopeViewModel CreateScopeViewModel(string scope, bool check)
//        {
//            return new ScopeViewModel
//            {
//                Name = scope,
//                DisplayName = scope,
//                Description = scope,
//                Emphasize = true,
//                Required = true,
//                Checked = check
//            };

//            //return new ScopeViewModel
//            //{
//            //    Name = scope.Name,
//            //    DisplayName = scope.DisplayName,
//            //    Description = scope.Description,
//            //    Emphasize = scope.Emphasize,
//            //    Required = scope.Required,
//            //    Checked = check || scope.Required
//            //};
//        }
//        private ScopeViewModel GetOfflineAccessScope(bool check)
//        {
           
//            return new ScopeViewModel
//            {
//                Name = IdentityServer4.IdentityServerConstants.StandardScopes.OfflineAccess,
//                DisplayName = ConsentOptions.OfflineAccessDisplayName,
//                Description = ConsentOptions.OfflineAccessDescription,
//                Emphasize = true,
//                Checked = check
//            };
//        }
//#endregion Device

//#region Diagnostics 诊断
//        public async Task<IActionResult> Diagnostics()
//        {
//            var localAddresses = new string[] { "127.0.0.1", "::1", HttpContext.Connection.LocalIpAddress.ToString() };
//            if (!localAddresses.Contains(HttpContext.Connection.RemoteIpAddress.ToString()))
//            {
//                return NotFound();
//            }

//            var model = new DiagnosticsViewModel(await HttpContext.AuthenticateAsync());
//            return View(model);
//        }
//#endregion Diagnostics 诊断

//#region Grants 同意，准予，允许 此示例控制器允许用户撤消授予客户端的授予
//        /// <summary>
//        /// Show list of grants
//        /// </summary>
//        [HttpGet]
//        public async Task<IActionResult> Grants()
//        {
//            return View("Index", await BuildViewModelAsync());
//        }

//        /// <summary>
//        /// Handle postback to revoke a client
//        /// </summary>
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Revoke(string clientId)
//        {
//            await _interaction.RevokeUserConsentAsync(clientId);
//            await _events.RaiseAsync(new GrantsRevokedEvent(User.GetSubjectId(), clientId));

//            return RedirectToAction("Index");
//        }

//        private async Task<GrantsViewModel> BuildViewModelAsync()
//        {
//#if Identity3_0
//            var grants = await _interaction.GetAllUserConsentsAsync();
//#else
//            var grants = await _interaction.GetAllUserGrantsAsync();
//#endif
//            var list = new List<GrantViewModel>();
//            foreach (var grant in grants)
//            {
//                var client = await _clientStore.FindClientByIdAsync(grant.ClientId);
//                if (client != null)
//                {
//                    var resources = await _resourceStore.FindResourcesByScopeAsync(grant.Scopes);

//                    var item = new GrantViewModel()
//                    {
//                        ClientId = client.ClientId,
//                        ClientName = client.ClientName ?? client.ClientId,
//                        ClientLogoUrl = client.LogoUri,
//                        ClientUrl = client.ClientUri,
//                        Created = grant.CreationTime,
//                        Expires = grant.Expiration,
//                        IdentityGrantNames = resources.IdentityResources.Select(x => x.DisplayName ?? x.Name).ToArray(),
//                        ApiGrantNames = resources.ApiResources.Select(x => x.DisplayName ?? x.Name).ToArray()
//                    };

//                    list.Add(item);
//                }
//            }

//            return new GrantsViewModel
//            {
//                Grants = list
//            };
//        }
//#endregion Grants

//    }


//}
