package com.utility.template;

import com.utility.util.FileUtil;

import java.util.HashMap;
import java.util.Iterator;
import java.util.Map;
import java.util.UUID;

public class CSharpTemplate {
    private  boolean docker=true;
    private    final  CsharpWebService csharpWebService=new CsharpWebService();
    private    final  CsharpWeb csharpWeb=new CsharpWeb();
    private    final  CsharpWebApi csharpWebApi=new CsharpWebApi();
    private boolean https=true;
    private UUID typeUuid=UUID.randomUUID();
    private UUID typeUuid1=UUID.randomUUID();
    private  String dir;

    private  class  CsharpWebCoreApi extends CsharpWeb{
        protected String[] dire=new String[]{"Properties"};
        protected String program;

        @Override
        protected void init(String program) {
            launchSettings();
            appsettings();
            appsettingsDevelopment();
            dockerfile();
            dockerignore();
            program();
            startup();
        }

        protected  void appsettings(){
            String appsettingsJson=json(true);
            files.put("appsettings.Development.json",appsettingsJson);
        }

        protected  void appsettingsDevelopment(){
            String appsettingsJson=json(false);
            files.put("appsettings.Development.json",appsettingsJson);
        }

        protected  String json(boolean app){
            String appsettingsJson="{\n" +
                    "  \"Logging\": {\n" +
                    "    \"LogLevel\": {\n" +
                    "      \"Default\": \"Information\",\n" +
                    "      \"Microsoft\": \"Warning\",\n" +
                    "      \"Microsoft.Hosting.Lifetime\": \"Information\"\n" +
                    "    }\n" +
                    "  }" +
                    (app?",\n  \"AllowedHosts\": \"*\"\n":"\n") +
                    "}\n";
           return  appsettingsJson;
        }

        protected  void  program(){
            String pro="using System;\n" +
                    "using System.Collections.Generic;\n" +
                    "using System.Linq;\n" +
                    "using System.Threading.Tasks;\n" +
                    "using Microsoft.AspNetCore.Hosting;\n" +
                    "using Microsoft.Extensions.Configuration;\n" +
                    "using Microsoft.Extensions.Hosting;\n" +
                    "using Microsoft.Extensions.Logging;\n" +
                    "\n" +
                    "namespace "+program+"\n" +
                    "{\n" +
                    "    public class Program\n" +
                    "    {\n" +
                    "        public static void Main(string[] args)\n" +
                    "        {\n" +
                    "            CreateHostBuilder(args).Build().Run();\n" +
                    "        }\n" +
                    "\n" +
                    "        public static IHostBuilder CreateHostBuilder(string[] args) =>\n" +
                    "            Host.CreateDefaultBuilder(args)\n" +
                    "                .ConfigureWebHostDefaults(webBuilder =>\n" +
                    "                {\n" +
                    "                    webBuilder.UseStartup<Startup>();\n" +
                    "                });\n" +
                    "    }\n" +
                    "}\n";
            files.put("Program.cs",pro);
        }

        protected void  startup(){
            String startup="using System;\n" +
                    "using System.Collections.Generic;\n" +
                    "using System.Linq;\n" +
                    "using System.Threading.Tasks;\n" +
                    "using Microsoft.AspNetCore.Builder;\n" +
                    "using Microsoft.AspNetCore.Hosting;\n" +
                    "using Microsoft.AspNetCore.Http;\n" +
                    "using Microsoft.Extensions.DependencyInjection;\n" +
                    "using Microsoft.Extensions.Hosting;\n" +
                    "\n" +
                    "namespace "+program+"\n" +
                    "{\n" +
                    "    public class Startup\n" +
                    "    {\n" +
                    "        // This method gets called by the runtime. Use this method to add services to the container.\n" +
                    "        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940\n" +
                    "        public void ConfigureServices(IServiceCollection services)\n" +
                    "        {\n" +
                    "        }\n" +
                    "\n" +
                    "        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.\n" +
                    "        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)\n" +
                    "        {\n" +
                    "            if (env.IsDevelopment())\n" +
                    "            {\n" +
                    "                app.UseDeveloperExceptionPage();\n" +
                    "            }\n" +
                    "\n" +
                    "            app.UseRouting();\n" +
                    "\n" +
                    "            app.UseEndpoints(endpoints =>\n" +
                    "            {\n" +
                    "                endpoints.MapGet(\"/\", async context =>\n" +
                    "                {\n" +
                    "                    await context.Response.WriteAsync(\"Hello World!\");\n" +
                    "                });\n" +
                    "            });\n" +
                    "        }\n" +
                    "    }\n" +
                    "}\n";
            files.put("Startup.cs",startup);
}

        protected  void  launchSettings(){
            String launchSettingsJson="{\n" +
                    "  \"iisSettings\": {\n" +
                    "    \"windowsAuthentication\": false,\n" +
                    "    \"anonymousAuthentication\": true,\n" +
                    "    \"iisExpress\": {\n" +
                    "      \"applicationUrl\": \"http://localhost:10382\",\n" +
                    "      \"sslPort\": 44316\n" +
                    "    }\n" +
                    "  },\n" +
                    "  \"profiles\": {\n" +
                    "    \"IIS Express\": {\n" +
                    "      \"commandName\": \"IISExpress\",\n" +
                    "      \"launchBrowser\": true,\n" +
                    "      \"environmentVariables\": {\n" +
                    "        \"ASPNETCORE_ENVIRONMENT\": \"Development\"\n" +
                    "      }\n" +
                    "    },\n" +
                    "    \"Shop.Core.Api\": {\n" +
                    "      \"commandName\": \"Project\",\n" +
                    "      \"launchBrowser\": true,\n" +
                    "      \"environmentVariables\": {\n" +
                    "        \"ASPNETCORE_ENVIRONMENT\": \"Development\"\n" +
                    "      },\n" +
                    "      \"applicationUrl\": \"https://localhost:5001;http://localhost:5000\"\n" +
                    "    }" +
                    (docker?(",\n    \"Docker\": {\n" +
                            "      \"commandName\": \"Docker\",\n" +
                            "      \"launchBrowser\": true,\n" +
                            "      \"launchUrl\": \"{Scheme}://{ServiceHost}:{ServicePort}\",\n" +
                            "      \"publishAllPorts\": true,\n" +
                            "      \"useSSL\": true\n" +
                            "    }\n" +
                            "  }\n"):"\n") +
                    "}";
            files.put("Properties/launchSettings.json", launchSettingsJson);
        }

        @Override
        protected void dockerfile() {
            String Dock="#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.\n" +
                    "\n" +
                    "#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.\n" +
                    "#For more information, please see https://aka.ms/containercompat\n" +
                    "\n" +
                    "FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-nanoserver-sac2016 AS base\n" +
                    "WORKDIR /app\n" +
                    "EXPOSE 80\n" +
                    "EXPOSE 443\n" +
                    "\n" +
                    "FROM mcr.microsoft.com/dotnet/core/sdk:3.1-nanoserver-sac2016 AS build\n" +
                    "WORKDIR /src\n" +
                    "COPY [\""+program+"/"+program+".csproj\", \""+program+"/\"]\n" +
                    "RUN dotnet restore \""+program+"/"+program+".csproj\"\n" +
                    "COPY . .\n" +
                    "WORKDIR \"/src/"+program+"\"\n" +
                    "RUN dotnet build \""+program+".csproj\" -c Release -o /app/build\n" +
                    "\n" +
                    "FROM build AS publish\n" +
                    "RUN dotnet publish \""+program+".csproj\" -c Release -o /app/publish\n" +
                    "\n" +
                    "FROM base AS final\n" +
                    "WORKDIR /app\n" +
                    "COPY --from=publish /app/publish .\n" +
                    "ENTRYPOINT [\"dotnet\", \""+program+".dll\"]";
            files.put("Dockerfile",Dock);
        }

        @Override
        protected void dockerignore() {
            String dockerignore="**/.classpath\n" +
                    "**/.dockerignore\n" +
                    "**/.env\n" +
                    "**/.git\n" +
                    "**/.gitignore\n" +
                    "**/.project\n" +
                    "**/.settings\n" +
                    "**/.toolstarget\n" +
                    "**/.vs\n" +
                    "**/.vscode\n" +
                    "**/*.*proj.user\n" +
                    "**/*.dbmdl\n" +
                    "**/*.jfm\n" +
                    "**/azds.yaml\n" +
                    "**/bin\n" +
                    "**/charts\n" +
                    "**/docker-compose*\n" +
                    "**/Dockerfile*\n" +
                    "**/node_modules\n" +
                    "**/npm-debug.log\n" +
                    "**/obj\n" +
                    "**/secrets.dev.yaml\n" +
                    "**/values.dev.yaml\n" +
                    "LICENSE\n" +
                    "README.md";
            files.put("dockerignore", dockerignore);
        }
    }

    private  class  CsharpWcf extends CsharpWeb{
        protected String[] dire=new String[]{"Properties"};

        @Override
        protected void init(String program) {
           assemblyInfo(program);
            appconfig();
        }

        private  void pro(String program){
            String progr="using System;\n" +
                    "using System.Collections.Generic;\n" +
                    "using System.Linq;\n" +
                    "using System.Text;\n" +
                    "using System.Threading.Tasks;\n" +
                    "\n" +
                    "namespace "+program+"\n" +
                    "{\n" +
                    "    class Program\n" +
                    "    {\n" +
                    "        static void Main(string[] args)\n" +
                    "        {\n" +
                    "        }\n" +
                    "    }\n" +
                    "}\n";
            files.put("Program.cs",progr);
        }

        private  void  appconfig(){
            String config="<?xml version=\"1.0\" encoding=\"utf-8\" ?>\n" +
                    "<configuration>\n" +
                    "    <startup> \n" +
                    "        <supportedRuntime version=\"v4.0\" sku=\".NETFramework,Version=v4.7.2\" />\n" +
                    "    </startup>\n" +
                    "</configuration>";
            files.put("App.config",config);
        }
        @Override
        protected void csproj(String program) {
            String cspr="<?xml version=\"1.0\" encoding=\"utf-8\"?>\n" +
                    "<Project ToolsVersion=\"15.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">\n" +
                    "  <Import Project=\"$(MSBuildExtensionsPath)\\$(MSBuildToolsVersion)\\Microsoft.Common.props\" Condition=\"Exists('$(MSBuildExtensionsPath)\\$(MSBuildToolsVersion)\\Microsoft.Common.props')\" />\n" +
                    "  <PropertyGroup>\n" +
                    "    <Configuration Condition=\" '$(Configuration)' == '' \">Debug</Configuration>\n" +
                    "    <Platform Condition=\" '$(Platform)' == '' \">AnyCPU</Platform>\n" +
                    "    <ProjectGuid>{"+uuid.toString().toUpperCase()+"}</ProjectGuid>\n" +
                    "    <OutputType>Exe</OutputType>\n" +
                    "    <RootNamespace>"+program+"</RootNamespace>\n" +
                    "    <AssemblyName>"+program+"</AssemblyName>\n" +
                    "    <TargetFrameworkVersion>v4.7.2</TargetFrameworkVersion>\n" +
                    "    <FileAlignment>512</FileAlignment>\n" +
                    "    <AutoGenerateBindingRedirects>true</AutoGenerateBindingRedirects>\n" +
                    "    <Deterministic>true</Deterministic>\n" +
                    "  </PropertyGroup>\n" +
                    "  <PropertyGroup Condition=\" '$(Configuration)|$(Platform)' == 'Debug|AnyCPU' \">\n" +
                    "    <PlatformTarget>AnyCPU</PlatformTarget>\n" +
                    "    <DebugSymbols>true</DebugSymbols>\n" +
                    "    <DebugType>full</DebugType>\n" +
                    "    <Optimize>false</Optimize>\n" +
                    "    <OutputPath>bin\\Debug\\</OutputPath>\n" +
                    "    <DefineConstants>DEBUG;TRACE</DefineConstants>\n" +
                    "    <ErrorReport>prompt</ErrorReport>\n" +
                    "    <WarningLevel>4</WarningLevel>\n" +
                    "  </PropertyGroup>\n" +
                    "  <PropertyGroup Condition=\" '$(Configuration)|$(Platform)' == 'Release|AnyCPU' \">\n" +
                    "    <PlatformTarget>AnyCPU</PlatformTarget>\n" +
                    "    <DebugType>pdbonly</DebugType>\n" +
                    "    <Optimize>true</Optimize>\n" +
                    "    <OutputPath>bin\\Release\\</OutputPath>\n" +
                    "    <DefineConstants>TRACE</DefineConstants>\n" +
                    "    <ErrorReport>prompt</ErrorReport>\n" +
                    "    <WarningLevel>4</WarningLevel>\n" +
                    "  </PropertyGroup>\n" +
                    "  <ItemGroup>\n" +
                    "    <Reference Include=\"System\" />\n" +
                    "    <Reference Include=\"System.Core\" />\n" +
                    "    <Reference Include=\"System.Xml.Linq\" />\n" +
                    "    <Reference Include=\"System.Data.DataSetExtensions\" />\n" +
                    "    <Reference Include=\"Microsoft.CSharp\" />\n" +
                    "    <Reference Include=\"System.Data\" />\n" +
                    "    <Reference Include=\"System.Net.Http\" />\n" +
                    "    <Reference Include=\"System.Xml\" />\n" +
                    "  </ItemGroup>\n" +
                    "  <ItemGroup>\n" +
                    "    <Compile Include=\"Program.cs\" />\n" +
                    "    <Compile Include=\"Properties\\AssemblyInfo.cs\" />\n" +
                    "  </ItemGroup>\n" +
                    "  <ItemGroup>\n" +
                    "    <None Include=\"App.config\" />\n" +
                    "  </ItemGroup>\n" +
                    "  <Import Project=\"$(MSBuildToolsPath)\\Microsoft.CSharp.targets\" />\n" +
                    "</Project>";
        }
    }

    private  class CsharpWebService extends CsharpWeb{
        protected String[] dire=new String[]{"Properties"};
        @Override
        protected void gloabl(String program) {

        }

        @Override
        protected void init(String program) {
            webService=true;
            super.init(program);
        }

    }

    private  class CsharpWebApi extends CsharpWeb{

        protected String[] dire=new String[]{"App_Data","Models","Properties","App_Start","Controllers"};

        @Override
        protected  void init(String program){
            webApiConfig(program);
            super.init(program);
        }

        protected void  webApiConfig(String program){
            String webApiConfigCs="using System;\n" +
                    "using System.Collections.Generic;\n" +
                    "using System.Linq;\n" +
                    "using System.Web.Http;\n" +
                    "\n" +
                    "namespace "+program+"\n" +
                    "{\n" +
                    "    public static class WebApiConfig\n" +
                    "    {\n" +
                    "        public static void Register(HttpConfiguration config)\n" +
                    "        {\n" +
                    "            // Web API 配置和服务\n" +
                    "\n" +
                    "            // Web API 路由\n" +
                    "            config.MapHttpAttributeRoutes();\n" +
                    "\n" +
                    "            config.Routes.MapHttpRoute(\n" +
                    "                name: \"DefaultApi\",\n" +
                    "                routeTemplate: \"api/{controller}/{id}\",\n" +
                    "                defaults: new { id = RouteParameter.Optional }\n" +
                    "            );\n" +
                    "        }\n" +
                    "    }\n" +
                    "}\n";
            files.put("App_Start/WebApiConfig.cs",webApiConfigCs);
        }

        @Override
        protected  void  gloabl(String program){
            String GlobalAsax="<%@ Application Codebehind=\"Global.asax.cs\" Inherits=\""+program+".Global\" Language=\"C#\" %>";
            files.put("Global.asax",GlobalAsax);
            String GlobalAsaxCS="using System;\n" +
                    "using System.Collections.Generic;\n" +
                    "using System.Linq;\n" +
                    "using System.Web;\n" +
                    "using System.Web.Http;\n" +
                    "using System.Web.Routing;\n" +
                    "\n" +
                    "namespace "+program+"\n" +
                    "{\n" +
                    "    public class WebApiApplication : System.Web.HttpApplication\n" +
                    "    {\n" +
                    "        protected void Application_Start()\n" +
                    "        {\n" +
                    "            GlobalConfiguration.Configure(WebApiConfig.Register);\n" +
                    "        }\n" +
                    "    }\n" +
                    "}\n";
            files.put("Global.asax.cs",GlobalAsaxCS);
        }
        @Override
        protected void webConfig() {
            String WebConfig="<?xml version=\"1.0\" encoding=\"utf-8\"?>\n" +
                    "<!--\n" +
                    "  有关如何配置 ASP.NET 应用程序的详细信息，请访问\n" +
                    "  https://go.microsoft.com/fwlink/?LinkId=301879\n" +
                    "  -->\n" +
                    "<configuration>\n" +
                    "  <appSettings></appSettings>\n" +
                    "  <system.web>\n" +
                    "    <compilation debug=\"true\" targetFramework=\"4.7.2\"/>\n" +
                    "    <httpRuntime targetFramework=\"4.7.2\"/>\n" +
                    "  </system.web>\n" +
                    "  <system.webServer>\n" +
                    "    <handlers>\n" +
                    "      <remove name=\"ExtensionlessUrlHandler-Integrated-4.0\"/>\n" +
                    "      <remove name=\"OPTIONSVerbHandler\"/>\n" +
                    "      <remove name=\"TRACEVerbHandler\"/>\n" +
                    "      <add name=\"ExtensionlessUrlHandler-Integrated-4.0\" path=\"*.\" verb=\"*\" type=\"System.Web.Handlers.TransferRequestHandler\"\n" +
                    "        preCondition=\"integratedMode,runtimeVersionv4.0\"/>\n" +
                    "    </handlers>\n" +
                    "  </system.webServer>\n" +
                    "  <runtime>\n" +
                    "    <assemblyBinding xmlns=\"urn:schemas-microsoft-com:asm.v1\">\n" +
                    "      <dependentAssembly>\n" +
                    "        <assemblyIdentity name=\"Newtonsoft.Json\" publicKeyToken=\"30ad4fe6b2a6aeed\"/>\n" +
                    "        <bindingRedirect oldVersion=\"0.0.0.0-12.0.0.0\" newVersion=\"12.0.0.0\"/>\n" +
                    "      </dependentAssembly>\n" +
                    "      <dependentAssembly>\n" +
                    "        <assemblyIdentity name=\"System.Web.Helpers\" publicKeyToken=\"31bf3856ad364e35\"/>\n" +
                    "        <bindingRedirect oldVersion=\"0.0.0.0-3.0.0.0\" newVersion=\"3.0.0.0\"/>\n" +
                    "      </dependentAssembly>\n" +
                    "      <dependentAssembly>\n" +
                    "        <assemblyIdentity name=\"System.Web.Mvc\" publicKeyToken=\"31bf3856ad364e35\"/>\n" +
                    "        <bindingRedirect oldVersion=\"0.0.0.0-5.2.7.0\" newVersion=\"5.2.7.0\"/>\n" +
                    "      </dependentAssembly>\n" +
                    "      <dependentAssembly>\n" +
                    "        <assemblyIdentity name=\"System.Web.WebPages\" publicKeyToken=\"31bf3856ad364e35\"/>\n" +
                    "        <bindingRedirect oldVersion=\"0.0.0.0-3.0.0.0\" newVersion=\"3.0.0.0\"/>\n" +
                    "      </dependentAssembly>\n" +
                    "    </assemblyBinding>\n" +
                    "  </runtime>\n" +
                    "  <system.codedom>\n" +
                    "    <compilers>\n" +
                    "      <compiler language=\"c#;cs;csharp\" extension=\".cs\"\n" +
                    "        type=\"Microsoft.CodeDom.Providers.DotNetCompilerPlatform.CSharpCodeProvider, Microsoft.CodeDom.Providers.DotNetCompilerPlatform, Version=2.0.1.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\"\n" +
                    "        warningLevel=\"4\" compilerOptions=\"/langversion:default /nowarn:1659;1699;1701\"/>\n" +
                    "      <compiler language=\"vb;vbs;visualbasic;vbscript\" extension=\".vb\"\n" +
                    "        type=\"Microsoft.CodeDom.Providers.DotNetCompilerPlatform.VBCodeProvider, Microsoft.CodeDom.Providers.DotNetCompilerPlatform, Version=2.0.1.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\"\n" +
                    "        warningLevel=\"4\" compilerOptions=\"/langversion:default /nowarn:41008 /define:_MYTYPE=\\&quot;Web\\&quot; /optionInfer+\"/>\n" +
                    "    </compilers>\n" +
                    "  </system.codedom>\n" +
                    "</configuration>\n";
            files.put("Web.config", WebConfig);
        }

        @Override
        protected void packagesConfig() {
            String pac="<?xml version=\"1.0\" encoding=\"utf-8\"?>\n" +
                    "<packages>\n" +
                    "  <package id=\"Microsoft.AspNet.WebApi\" version=\"5.2.7\" targetFramework=\"net472\" />\n" +
                    "  <package id=\"Microsoft.AspNet.WebApi.Client\" version=\"5.2.7\" targetFramework=\"net472\" />\n" +
                    "  <package id=\"Microsoft.AspNet.WebApi.Client.zh-Hans\" version=\"5.2.7\" targetFramework=\"net472\" />\n" +
                    "  <package id=\"Microsoft.AspNet.WebApi.Core\" version=\"5.2.7\" targetFramework=\"net472\" />\n" +
                    "  <package id=\"Microsoft.AspNet.WebApi.Core.zh-Hans\" version=\"5.2.7\" targetFramework=\"net472\" />\n" +
                    "  <package id=\"Microsoft.AspNet.WebApi.WebHost\" version=\"5.2.7\" targetFramework=\"net472\" />\n" +
                    "  <package id=\"Microsoft.AspNet.WebApi.WebHost.zh-Hans\" version=\"5.2.7\" targetFramework=\"net472\" />\n" +
                    "  <package id=\"Microsoft.CodeDom.Providers.DotNetCompilerPlatform\" version=\"2.0.1\" targetFramework=\"net472\" />\n" +
                    "  <package id=\"Microsoft.VisualStudio.Azure.Containers.Tools.Targets\" version=\"1.10.8\" targetFramework=\"net472\" />\n" +
                    "  <package id=\"Newtonsoft.Json\" version=\"12.0.2\" targetFramework=\"net472\" />\n" +
                    "</packages>";
            files.put("packages.config", pac);
        }

        @Override
        protected void csproj(String program) {
            String cspr="<Project ToolsVersion=\"15.0\" DefaultTargets=\"Build\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">\n" +
                    "  <Import Project=\"..\\packages\\Microsoft.VisualStudio.Azure.Containers.Tools.Targets.1.10.8\\build\\Microsoft.VisualStudio.Azure.Containers.Tools.Targets.props\" Condition=\"Exists('..\\packages\\Microsoft.VisualStudio.Azure.Containers.Tools.Targets.1.10.8\\build\\Microsoft.VisualStudio.Azure.Containers.Tools.Targets.props')\" />\n" +
                    "  <Import Project=\"..\\packages\\Microsoft.CodeDom.Providers.DotNetCompilerPlatform.2.0.1\\build\\net46\\Microsoft.CodeDom.Providers.DotNetCompilerPlatform.props\" Condition=\"Exists('..\\packages\\Microsoft.CodeDom.Providers.DotNetCompilerPlatform.2.0.1\\build\\net46\\Microsoft.CodeDom.Providers.DotNetCompilerPlatform.props')\" />\n" +
                    "  <Import Project=\"$(MSBuildExtensionsPath)\\$(MSBuildToolsVersion)\\Microsoft.Common.props\" Condition=\"Exists('$(MSBuildExtensionsPath)\\$(MSBuildToolsVersion)\\Microsoft.Common.props')\" />\n" +
                    "  <PropertyGroup>\n" +
                    "    <Configuration Condition=\" '$(Configuration)' == '' \">Debug</Configuration>\n" +
                    "    <Platform Condition=\" '$(Platform)' == '' \">AnyCPU</Platform>\n" +
                    "    <ProductVersion>\n" +
                    "    </ProductVersion>\n" +
                    "    <SchemaVersion>2.0</SchemaVersion>\n" +
                    "    <ProjectGuid>{"+uuid.toString().toUpperCase()+"}</ProjectGuid>\n" +
                    "    <ProjectTypeGuids>{"+typeUuid+"};{"+typeUuid1+"}</ProjectTypeGuids>\n" +
                    "    <OutputType>Library</OutputType>\n" +
                    "    <AppDesignerFolder>Properties</AppDesignerFolder>\n" +
                    "    <RootNamespace>"+program+"</RootNamespace>\n" +
                    "    <AssemblyName>"+program+"</AssemblyName>\n" +
                    "    <TargetFrameworkVersion>v4.7.2</TargetFrameworkVersion>\n" +
                    "    <UseIISExpress>true</UseIISExpress>\n" +
                    "    <Use64BitIISExpress />\n" +
                    "    <IISExpressSSLPort>44345</IISExpressSSLPort>\n" +
                    "    <IISExpressAnonymousAuthentication />\n" +
                    "    <IISExpressWindowsAuthentication />\n" +
                    "    <IISExpressUseClassicPipelineMode />\n" +
                    "    <UseGlobalApplicationHostFile />\n" +
                    "    <NuGetPackageImportStamp>\n" +
                    "    </NuGetPackageImportStamp>\n" +
                    "    <DockerLaunchAction>LaunchBrowser</DockerLaunchAction>\n" +
                    "    <DockerLaunchUrl>http://{ServiceIPAddress}</DockerLaunchUrl>\n" +
                    "  </PropertyGroup>\n" +
                    "  <PropertyGroup Condition=\" '$(Configuration)|$(Platform)' == 'Debug|AnyCPU' \">\n" +
                    "    <DebugSymbols>true</DebugSymbols>\n" +
                    "    <DebugType>full</DebugType>\n" +
                    "    <Optimize>false</Optimize>\n" +
                    "    <OutputPath>bin\\</OutputPath>\n" +
                    "    <DefineConstants>DEBUG;TRACE</DefineConstants>\n" +
                    "    <ErrorReport>prompt</ErrorReport>\n" +
                    "    <WarningLevel>4</WarningLevel>\n" +
                    "  </PropertyGroup>\n" +
                    "  <PropertyGroup Condition=\" '$(Configuration)|$(Platform)' == 'Release|AnyCPU' \">\n" +
                    "    <DebugSymbols>true</DebugSymbols>\n" +
                    "    <DebugType>pdbonly</DebugType>\n" +
                    "    <Optimize>true</Optimize>\n" +
                    "    <OutputPath>bin\\</OutputPath>\n" +
                    "    <DefineConstants>TRACE</DefineConstants>\n" +
                    "    <ErrorReport>prompt</ErrorReport>\n" +
                    "    <WarningLevel>4</WarningLevel>\n" +
                    "  </PropertyGroup>\n" +
                    "  <ItemGroup>\n" +
                    "    <Reference Include=\"Microsoft.CSharp\" />\n" +
                    "    <Reference Include=\"System.Net.Http\" />\n" +
                    "    <Reference Include=\"System.Web.DynamicData\" />\n" +
                    "    <Reference Include=\"System.Web.Entity\" />\n" +
                    "    <Reference Include=\"System.Web.ApplicationServices\" />\n" +
                    "    <Reference Include=\"System.ComponentModel.DataAnnotations\" />\n" +
                    "    <Reference Include=\"System\" />\n" +
                    "    <Reference Include=\"System.Data\" />\n" +
                    "    <Reference Include=\"System.Core\" />\n" +
                    "    <Reference Include=\"System.Data.DataSetExtensions\" />\n" +
                    "    <Reference Include=\"System.Web.Extensions\" />\n" +
                    "    <Reference Include=\"System.Xml.Linq\" />\n" +
                    "    <Reference Include=\"System.Drawing\" />\n" +
                    "    <Reference Include=\"System.Web\" />\n" +
                    "    <Reference Include=\"System.Xml\" />\n" +
                    "    <Reference Include=\"System.Configuration\" />\n" +
                    "    <Reference Include=\"System.Web.Services\" />\n" +
                    "    <Reference Include=\"System.EnterpriseServices\" />\n" +
                    "  </ItemGroup>\n" +
                    "  <ItemGroup>\n" +
                    "    <Reference Include=\"Newtonsoft.Json\">\n" +
                    "      <HintPath>..\\packages\\Newtonsoft.Json.12.0.2\\lib\\net45\\Newtonsoft.Json.dll</HintPath>\n" +
                    "    </Reference>\n" +
                    "    <Reference Include=\"System.Net.Http.Formatting\">\n" +
                    "      <HintPath>..\\packages\\Microsoft.AspNet.WebApi.Client.5.2.7\\lib\\net45\\System.Net.Http.Formatting.dll</HintPath>\n" +
                    "    </Reference>\n" +
                    "    <Reference Include=\"System.Web.Http\">\n" +
                    "      <HintPath>..\\packages\\Microsoft.AspNet.WebApi.Core.5.2.7\\lib\\net45\\System.Web.Http.dll</HintPath>\n" +
                    "    </Reference>\n" +
                    "    <Reference Include=\"System.Web.Http.WebHost\">\n" +
                    "      <HintPath>..\\packages\\Microsoft.AspNet.WebApi.WebHost.5.2.7\\lib\\net45\\System.Web.Http.WebHost.dll</HintPath>\n" +
                    "    </Reference>\n" +
                    "    <Reference Include=\"Microsoft.CodeDom.Providers.DotNetCompilerPlatform\">\n" +
                    "      <HintPath>..\\packages\\Microsoft.CodeDom.Providers.DotNetCompilerPlatform.2.0.1\\lib\\net45\\Microsoft.CodeDom.Providers.DotNetCompilerPlatform.dll</HintPath>\n" +
                    "    </Reference>\n" +
                    "  </ItemGroup>\n" +
                    "  <ItemGroup>\n" +
                    "    <Content Include=\"Global.asax\" />\n" +
                    "    <Content Include=\"Web.config\" />\n" +
                    "  </ItemGroup>\n" +
                    "  <ItemGroup>\n" +
                    "    <Compile Include=\"App_Start\\WebApiConfig.cs\" />\n" +
                    "    <Compile Include=\"Global.asax.cs\">\n" +
                    "      <DependentUpon>Global.asax</DependentUpon>\n" +
                    "    </Compile>\n" +
                    "    <Compile Include=\"Properties\\AssemblyInfo.cs\" />\n" +
                    "  </ItemGroup>\n" +
                    "  <ItemGroup>\n" +
                    (docker?("    <None Include=\"Dockerfile\" />\n" +
                            "    <None Include=\".dockerignore\">\n" +
                            "      <DependentUpon>Dockerfile</DependentUpon>\n" +
                            "    </None>\n"):"") +
                    "    <None Include=\"packages.config\" />\n" +
                    "    <None Include=\"Web.Debug.config\">\n" +
                    "      <DependentUpon>Web.config</DependentUpon>\n" +
                    "    </None>\n" +
                    "    <None Include=\"Web.Release.config\">\n" +
                    "      <DependentUpon>Web.config</DependentUpon>\n" +
                    "    </None>\n" +
                    "  </ItemGroup>\n" +
                    "  <ItemGroup>\n" +
                    "    <Folder Include=\"App_Data\\\" />\n" +
                    "    <Folder Include=\"Controllers\\\" />\n" +
                    "    <Folder Include=\"Models\\\" />\n" +
                    "  </ItemGroup>\n" +
                    "  <PropertyGroup>\n" +
                    "    <VisualStudioVersion Condition=\"'$(VisualStudioVersion)' == ''\">10.0</VisualStudioVersion>\n" +
                    "    <VSToolsPath Condition=\"'$(VSToolsPath)' == ''\">$(MSBuildExtensionsPath32)\\Microsoft\\VisualStudio\\v$(VisualStudioVersion)</VSToolsPath>\n" +
                    "  </PropertyGroup>\n" +
                    "  <Import Project=\"$(MSBuildBinPath)\\Microsoft.CSharp.targets\" />\n" +
                    "  <Import Project=\"$(VSToolsPath)\\WebApplications\\Microsoft.WebApplication.targets\" Condition=\"'$(VSToolsPath)' != ''\" />\n" +
                    "  <Import Project=\"$(MSBuildExtensionsPath32)\\Microsoft\\VisualStudio\\v10.0\\WebApplications\\Microsoft.WebApplication.targets\" Condition=\"false\" />\n" +
                    "  <ProjectExtensions>\n" +
                    "    <VisualStudio>\n" +
                    "      <FlavorProperties GUID=\"{"+typeUuid+"}\">\n" +
                    "        <WebProjectProperties>\n" +
                    "          <UseIIS>True</UseIIS>\n" +
                    "          <AutoAssignPort>True</AutoAssignPort>\n" +
                    "          <DevelopmentServerPort>8937</DevelopmentServerPort>\n" +
                    "          <DevelopmentServerVPath>/</DevelopmentServerVPath>\n" +
                    "          <IISUrl>https://localhost:44345/</IISUrl>\n" +
                    "          <NTLMAuthentication>False</NTLMAuthentication>\n" +
                    "          <UseCustomServer>False</UseCustomServer>\n" +
                    "          <CustomServerUrl>\n" +
                    "          </CustomServerUrl>\n" +
                    "          <SaveServerSettingsInUserFile>False</SaveServerSettingsInUserFile>\n" +
                    "        </WebProjectProperties>\n" +
                    "      </FlavorProperties>\n" +
                    "    </VisualStudio>\n" +
                    "  </ProjectExtensions>\n" +
                    "  <Target Name=\"EnsureNuGetPackageBuildImports\" BeforeTargets=\"PrepareForBuild\">\n" +
                    "    <PropertyGroup>\n" +
                    "      <ErrorText>这台计算机上缺少此项目引用的 NuGet 程序包。使用“NuGet 程序包还原”可下载这些程序包。有关更多信息，请参见 http://go.microsoft.com/fwlink/?LinkID=322105。缺少的文件是 {0}。</ErrorText>\n" +
                    "    </PropertyGroup>\n" +
                    "    <Error Condition=\"!Exists('..\\packages\\Microsoft.CodeDom.Providers.DotNetCompilerPlatform.2.0.1\\build\\net46\\Microsoft.CodeDom.Providers.DotNetCompilerPlatform.props')\" Text=\"$([System.String]::Format('$(ErrorText)', '..\\packages\\Microsoft.CodeDom.Providers.DotNetCompilerPlatform.2.0.1\\build\\net46\\Microsoft.CodeDom.Providers.DotNetCompilerPlatform.props'))\" />\n" +
                    "    <Error Condition=\"!Exists('..\\packages\\Microsoft.VisualStudio.Azure.Containers.Tools.Targets.1.10.8\\build\\Microsoft.VisualStudio.Azure.Containers.Tools.Targets.props')\" Text=\"$([System.String]::Format('$(ErrorText)', '..\\packages\\Microsoft.VisualStudio.Azure.Containers.Tools.Targets.1.10.8\\build\\Microsoft.VisualStudio.Azure.Containers.Tools.Targets.props'))\" />\n" +
                    "    <Error Condition=\"!Exists('..\\packages\\Microsoft.VisualStudio.Azure.Containers.Tools.Targets.1.10.8\\build\\Microsoft.VisualStudio.Azure.Containers.Tools.Targets.targets')\" Text=\"$([System.String]::Format('$(ErrorText)', '..\\packages\\Microsoft.VisualStudio.Azure.Containers.Tools.Targets.1.10.8\\build\\Microsoft.VisualStudio.Azure.Containers.Tools.Targets.targets'))\" />\n" +
                    "  </Target>\n" +
                    "  <Import Project=\"..\\packages\\Microsoft.VisualStudio.Azure.Containers.Tools.Targets.1.10.8\\build\\Microsoft.VisualStudio.Azure.Containers.Tools.Targets.targets\" Condition=\"Exists('..\\packages\\Microsoft.VisualStudio.Azure.Containers.Tools.Targets.1.10.8\\build\\Microsoft.VisualStudio.Azure.Containers.Tools.Targets.targets')\" />\n" +
                    "  <!-- To modify your build process, add your task inside one of the targets below and uncomment it. \n" +
                    "       Other similar extension points exist, see Microsoft.Common.targets.\n" +
                    "  <Target Name=\"BeforeBuild\">\n" +
                    "  </Target>\n" +
                    "  <Target Name=\"AfterBuild\">\n" +
                    "  </Target>\n" +
                    "  -->\n" +
                    "</Project>";
            files.put(program+".csproj", cspr);
        }


    }

    private  class CsharpWeb{
        protected UUID uuid=UUID.randomUUID();
        protected  boolean webService=false;
        protected String[] dire=new String[]{"App_Data","Models","Properties"};
        protected Map<String,String> files=new HashMap<>();
        protected  void init(String program){
            assemblyInfo(program);
            dockerfile();
            dockerignore();
            packagesConfig();
            csproj(program);
            csprojUser(program);
            webConfig();
            webDebugConfig();
            webReleaseConfig();
        }

        public  void  generator(String program){
            FileUtil.create(dir);
            String pro=dir+"/"+program;
            FileUtil.create(pro);
            for (String di :dire) {
                FileUtil.create(pro+"/"+di);
            }
            Iterator<String> iterator=files.keySet().iterator();
            while ((iterator.hasNext())) {
                String file=iterator.next();
                String val=files.get(file);
                FileUtil.write(pro+"/"+file,val,false);
            }
        }

        protected  void assemblyInfo(String program){
            String assemblyInfoCs="using System.Reflection;\n" +
                    "using System.Runtime.CompilerServices;\n" +
                    "using System.Runtime.InteropServices;\n" +
                    "\n" +
                    "// 有关程序集的常规信息通过下列特性集\n" +
                    "// 控制。更改这些特性值可修改\n" +
                    "// 与程序集关联的信息。\n" +
                    "[assembly: AssemblyTitle(\""+program+"\")]\n" +
                    "[assembly: AssemblyDescription(\"\")]\n" +
                    "[assembly: AssemblyConfiguration(\"\")]\n" +
                    "[assembly: AssemblyCompany(\"Home\")]\n" +
                    "[assembly: AssemblyProduct(\""+program+"\")]\n" +
                    "[assembly: AssemblyCopyright(\"Copyright © Home 2020\")]\n" +
                    "[assembly: AssemblyTrademark(\"\")]\n" +
                    "[assembly: AssemblyCulture(\"\")]\n" +
                    "\n" +
                    "// 将 ComVisible 设置为 false 会使此程序集中的类型\n" +
                    "// 对 COM 组件不可见。如果需要\n" +
                    "// 从 COM 访问此程序集中的某个类型，请针对该类型将 ComVisible 特性设置为 true。\n" +
                    "[assembly: ComVisible(false)]\n" +
                    "\n" +
                    "// 如果此项目向 COM 公开，则下列 GUID 用于 typelib 的 ID\n" +
                    "[assembly: Guid(\""+uuid+"\")]\n" +
                    "\n" +
                    "// 程序集的版本信息由下列四个值组成:\n" +
                    "//\n" +
                    "//      主版本\n" +
                    "//      次版本\n" +
                    "//      内部版本号\n" +
                    "//      修订版本\n" +
                    "//\n" +
                    "// 可以指定所有值，也可以使用“修订号”和“内部版本号”的默认值，\n" +
                    "// 方法是按如下所示使用 \"*\":\n" +
                    "[assembly: AssemblyVersion(\"1.0.0.0\")]\n" +
                    "[assembly: AssemblyFileVersion(\"1.0.0.0\")]\n";
            files.put("Properties/AssemblyInfo.cs","assemblyInfoCs");
        }

        protected  void  dockerfile(){
            if(docker){
                String Dockerfile = "#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.\n" +
                        "#For more information, please see https://aka.ms/containercompat \n" +
                        "\n" +
                        "FROM mcr.microsoft.com/dotnet/framework/aspnet:4.8-windowsservercore-ltsc2016\n" +
                        "ARG source\n" +
                        "WORKDIR /inetpub/wwwroot\n" +
                        "COPY ${source:-obj/Docker/publish} .";
                files.put("Dockerfile", Dockerfile);

            }
        }

        protected  void  dockerignore(){
            String dockerignore = "*\n" +
                    "!obj\\Docker\\publish\\*\n" +
                    "!obj\\Docker\\empty\\";
            files.put(".dockerignore", dockerignore);
        }

        protected  void  gloabl(String program){
            String GlobalAsax="<%@ Application Codebehind=\"Global.asax.cs\" Inherits=\""+program+".Global\" Language=\"C#\" %>";
            files.put("Global.asax",GlobalAsax);
            String GlobalAsaxCS="using System;\n" +
                    "using System.Collections.Generic;\n" +
                    "using System.Linq;\n" +
                    "using System.Web;\n" +
                    "using System.Web.Security;\n" +
                    "using System.Web.SessionState;\n" +
                    "\n" +
                    "namespace "+program+"b\n" +
                    "{\n" +
                    "    public class Global : System.Web.HttpApplication\n" +
                    "    {\n" +
                    "        protected void Application_Start(object sender, EventArgs e)\n" +
                    "        {\n" +
                    "        }\n" +
                    "    }\n" +
                    "}";
            files.put("Global.asax.cs",GlobalAsaxCS);
        }

        protected  void  packagesConfig(){
            String packagesConfig="<?xml version=\"1.0\" encoding=\"utf-8\"?>\n" +
                    "<packages>\n" +
                    "  <package id=\"Microsoft.CodeDom.Providers.DotNetCompilerPlatform\" version=\"2.0.1\" targetFramework=\"net472\" />\n" +
                    "  <package id=\"Microsoft.VisualStudio.Azure.Containers.Tools.Targets\" version=\"1.10.8\" targetFramework=\"net472\" />\n" +
                    "</packages>";
            files.put("packages.config", packagesConfig);
        }

        protected void  csproj(String program){
            String cspr="<Project ToolsVersion=\"15.0\" DefaultTargets=\"Build\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">\n" +
                    "  <Import Project=\"..\\packages\\Microsoft.VisualStudio.Azure.Containers.Tools.Targets.1.10.8\\build\\Microsoft.VisualStudio.Azure.Containers.Tools.Targets.props\" Condition=\"Exists('..\\packages\\Microsoft.VisualStudio.Azure.Containers.Tools.Targets.1.10.8\\build\\Microsoft.VisualStudio.Azure.Containers.Tools.Targets.props')\" />\n" +
                    "  <Import Project=\"..\\packages\\Microsoft.CodeDom.Providers.DotNetCompilerPlatform.2.0.1\\build\\net46\\Microsoft.CodeDom.Providers.DotNetCompilerPlatform.props\" Condition=\"Exists('..\\packages\\Microsoft.CodeDom.Providers.DotNetCompilerPlatform.2.0.1\\build\\net46\\Microsoft.CodeDom.Providers.DotNetCompilerPlatform.props')\" />\n" +
                    "  <Import Project=\"$(MSBuildExtensionsPath)\\$(MSBuildToolsVersion)\\Microsoft.Common.props\" Condition=\"Exists('$(MSBuildExtensionsPath)\\$(MSBuildToolsVersion)\\Microsoft.Common.props')\" />\n" +
                    "  <PropertyGroup>\n" +
                    "    <Configuration Condition=\" '$(Configuration)' == '' \">Debug</Configuration>\n" +
                    "    <Platform Condition=\" '$(Platform)' == '' \">AnyCPU</Platform>\n" +
                    "    <ProductVersion>\n" +
                    "    </ProductVersion>\n" +
                    "    <SchemaVersion>2.0</SchemaVersion>\n" +
                    "    <ProjectGuid>{"+uuid.toString().toUpperCase()+"}</ProjectGuid>\n" +
                    "    <ProjectTypeGuids>{"+typeUuid+"};{"+typeUuid1+"}</ProjectTypeGuids>\n" +
                    "    <OutputType>Library</OutputType>\n" +
                    "    <AppDesignerFolder>Properties</AppDesignerFolder>\n" +
                    "    <RootNamespace>"+program+"</RootNamespace>\n" +
                    "    <AssemblyName>"+ program+"</AssemblyName>\n" +
                    "    <TargetFrameworkVersion>v4.7.2</TargetFrameworkVersion>\n" +
                    "    <UseIISExpress>true</UseIISExpress>\n" +
                    "    <Use64BitIISExpress />\n" +
                    "    <IISExpressSSLPort>44363</IISExpressSSLPort>\n" +
                    "    <IISExpressAnonymousAuthentication />\n" +
                    "    <IISExpressWindowsAuthentication />\n" +
                    "    <IISExpressUseClassicPipelineMode />\n" +
                    "    <UseGlobalApplicationHostFile />\n" +
                    "    <NuGetPackageImportStamp>\n" +
                    "    </NuGetPackageImportStamp>\n" +
                    (docker?( "    <DockerLaunchAction>LaunchBrowser</DockerLaunchAction>\n" +
                            "    <DockerLaunchUrl>http://{ServiceIPAddress}</DockerLaunchUrl>\n"):"") +
                    "  </PropertyGroup>\n" +
                    "  <PropertyGroup Condition=\" '$(Configuration)|$(Platform)' == 'Debug|AnyCPU' \">\n" +
                    "    <DebugSymbols>true</DebugSymbols>\n" +
                    "    <DebugType>full</DebugType>\n" +
                    "    <Optimize>false</Optimize>\n" +
                    "    <OutputPath>bin\\</OutputPath>\n" +
                    "    <DefineConstants>DEBUG;TRACE</DefineConstants>\n" +
                    "    <ErrorReport>prompt</ErrorReport>\n" +
                    "    <WarningLevel>4</WarningLevel>\n" +
                    "  </PropertyGroup>\n" +
                    "  <PropertyGroup Condition=\" '$(Configuration)|$(Platform)' == 'Release|AnyCPU' \">\n" +
                    "    <DebugSymbols>true</DebugSymbols>\n" +
                    "    <DebugType>pdbonly</DebugType>\n" +
                    "    <Optimize>true</Optimize>\n" +
                    "    <OutputPath>bin\\</OutputPath>\n" +
                    "    <DefineConstants>TRACE</DefineConstants>\n" +
                    "    <ErrorReport>prompt</ErrorReport>\n" +
                    "    <WarningLevel>4</WarningLevel>\n" +
                    "  </PropertyGroup>\n" +
                    "  <ItemGroup>\n" +
                    "    <Reference Include=\"Microsoft.CSharp\" />\n" +
                    "    <Reference Include=\"System.Web.DynamicData\" />\n" +
                    "    <Reference Include=\"System.Web.Entity\" />\n" +
                    "    <Reference Include=\"System.Web.ApplicationServices\" />\n" +
                    "    <Reference Include=\"System.ComponentModel.DataAnnotations\" />\n" +
                    "    <Reference Include=\"System\" />\n" +
                    "    <Reference Include=\"System.Data\" />\n" +
                    "    <Reference Include=\"System.Core\" />\n" +
                    "    <Reference Include=\"System.Data.DataSetExtensions\" />\n" +
                    "    <Reference Include=\"System.Web.Extensions\" />\n" +
                    "    <Reference Include=\"System.Xml.Linq\" />\n" +
                    "    <Reference Include=\"System.Drawing\" />\n" +
                    "    <Reference Include=\"System.Web\" />\n" +
                    "    <Reference Include=\"System.Xml\" />\n" +
                    "    <Reference Include=\"System.Configuration\" />\n" +
                    "    <Reference Include=\"System.Web.Services\" />\n" +
                    "    <Reference Include=\"System.EnterpriseServices\" />\n" +
                    "  </ItemGroup>\n" +
                    "  <ItemGroup>\n" +
                    "    <Reference Include=\"Microsoft.CodeDom.Providers.DotNetCompilerPlatform\">\n" +
                    "      <HintPath>..\\packages\\Microsoft.CodeDom.Providers.DotNetCompilerPlatform.2.0.1\\lib\\net45\\Microsoft.CodeDom.Providers.DotNetCompilerPlatform.dll</HintPath>\n" +
                    "    </Reference>\n" +
                    "  </ItemGroup>\n" +
                    "  <ItemGroup>\n" +
                    "    <Content Include=\"Global.asax\" />\n" +
                    "    <Content Include=\"Web.config\" />\n" +
                    "  </ItemGroup>\n" +
                    "  <ItemGroup>\n" +
                    (webService?"":(  "    <Compile Include=\"Global.asax.cs\">\n" +
                            "      <DependentUpon>Global.asax</DependentUpon>\n" +
                            "    </Compile>\n")) +
                    "    <Compile Include=\"Properties\\AssemblyInfo.cs\" />\n" +
                    "  </ItemGroup>\n" +
                    "  <ItemGroup>\n" +
                    (docker?("    <None Include=\"Dockerfile\" />\n" +
                            "    <None Include=\".dockerignore\">\n" +
                            "      <DependentUpon>Dockerfile</DependentUpon>\n" +
                            "    </None>\n"):"")
                     +
                    "    <None Include=\"packages.config\" />\n" +
                    "    <None Include=\"Web.Debug.config\">\n" +
                    "      <DependentUpon>Web.config</DependentUpon>\n" +
                    "    </None>\n" +
                    "    <None Include=\"Web.Release.config\">\n" +
                    "      <DependentUpon>Web.config</DependentUpon>\n" +
                    "    </None>\n" +
                    "  </ItemGroup>\n" +
                    "  <ItemGroup>\n" +
                    "    <Folder Include=\"App_Data\\\" />\n" +
                    "    <Folder Include=\"Models\\\" />\n" +
                    "  </ItemGroup>\n" +
                    "  <PropertyGroup>\n" +
                    "    <VisualStudioVersion Condition=\"'$(VisualStudioVersion)' == ''\">10.0</VisualStudioVersion>\n" +
                    "    <VSToolsPath Condition=\"'$(VSToolsPath)' == ''\">$(MSBuildExtensionsPath32)\\Microsoft\\VisualStudio\\v$(VisualStudioVersion)</VSToolsPath>\n" +
                    "  </PropertyGroup>\n" +
                    "  <Import Project=\"$(MSBuildBinPath)\\Microsoft.CSharp.targets\" />\n" +
                    "  <Import Project=\"$(VSToolsPath)\\WebApplications\\Microsoft.WebApplication.targets\" Condition=\"'$(VSToolsPath)' != ''\" />\n" +
                    "  <Import Project=\"$(MSBuildExtensionsPath32)\\Microsoft\\VisualStudio\\v10.0\\WebApplications\\Microsoft.WebApplication.targets\" Condition=\"false\" />\n" +
                    "  <ProjectExtensions>\n" +
                    "    <VisualStudio>\n" +
                    "      <FlavorProperties GUID=\"{"+typeUuid+"}\">\n" +
                    "        <WebProjectProperties>\n" +
                    "          <UseIIS>True</UseIIS>\n" +
                    "          <AutoAssignPort>True</AutoAssignPort>\n" +
                    "          <DevelopmentServerPort>8828</DevelopmentServerPort>\n" +
                    "          <DevelopmentServerVPath>/</DevelopmentServerVPath>\n" +
                    "          <IISUrl>https://localhost:44363/</IISUrl>\n" +
                    "          <NTLMAuthentication>False</NTLMAuthentication>\n" +
                    "          <UseCustomServer>False</UseCustomServer>\n" +
                    "          <CustomServerUrl>\n" +
                    "          </CustomServerUrl>\n" +
                    "          <SaveServerSettingsInUserFile>False</SaveServerSettingsInUserFile>\n" +
                    "        </WebProjectProperties>\n" +
                    "      </FlavorProperties>\n" +
                    "    </VisualStudio>\n" +
                    "  </ProjectExtensions>\n" +
                    "  <Target Name=\"EnsureNuGetPackageBuildImports\" BeforeTargets=\"PrepareForBuild\">\n" +
                    "    <PropertyGroup>\n" +
                    "      <ErrorText>这台计算机上缺少此项目引用的 NuGet 程序包。使用“NuGet 程序包还原”可下载这些程序包。有关更多信息，请参见 http://go.microsoft.com/fwlink/?LinkID=322105。缺少的文件是 {0}。</ErrorText>\n" +
                    "    </PropertyGroup>\n" +
                    "    <Error Condition=\"!Exists('..\\packages\\Microsoft.CodeDom.Providers.DotNetCompilerPlatform.2.0.1\\build\\net46\\Microsoft.CodeDom.Providers.DotNetCompilerPlatform.props')\" Text=\"$([System.String]::Format('$(ErrorText)', '..\\packages\\Microsoft.CodeDom.Providers.DotNetCompilerPlatform.2.0.1\\build\\net46\\Microsoft.CodeDom.Providers.DotNetCompilerPlatform.props'))\" />\n" +
                    "    <Error Condition=\"!Exists('..\\packages\\Microsoft.VisualStudio.Azure.Containers.Tools.Targets.1.10.8\\build\\Microsoft.VisualStudio.Azure.Containers.Tools.Targets.props')\" Text=\"$([System.String]::Format('$(ErrorText)', '..\\packages\\Microsoft.VisualStudio.Azure.Containers.Tools.Targets.1.10.8\\build\\Microsoft.VisualStudio.Azure.Containers.Tools.Targets.props'))\" />\n" +
                    "    <Error Condition=\"!Exists('..\\packages\\Microsoft.VisualStudio.Azure.Containers.Tools.Targets.1.10.8\\build\\Microsoft.VisualStudio.Azure.Containers.Tools.Targets.targets')\" Text=\"$([System.String]::Format('$(ErrorText)', '..\\packages\\Microsoft.VisualStudio.Azure.Containers.Tools.Targets.1.10.8\\build\\Microsoft.VisualStudio.Azure.Containers.Tools.Targets.targets'))\" />\n" +
                    "  </Target>\n" +
                    "  <Import Project=\"..\\packages\\Microsoft.VisualStudio.Azure.Containers.Tools.Targets.1.10.8\\build\\Microsoft.VisualStudio.Azure.Containers.Tools.Targets.targets\" Condition=\"Exists('..\\packages\\Microsoft.VisualStudio.Azure.Containers.Tools.Targets.1.10.8\\build\\Microsoft.VisualStudio.Azure.Containers.Tools.Targets.targets')\" />\n" +
                    "  <!-- To modify your build process, add your task inside one of the targets below and uncomment it. \n" +
                    "       Other similar extension points exist, see Microsoft.Common.targets.\n" +
                    "  <Target Name=\"BeforeBuild\">\n" +
                    "  </Target>\n" +
                    "  <Target Name=\"AfterBuild\">\n" +
                    "  </Target>\n" +
                    "  -->\n" +
                    "</Project>";
            files.put(program+".csproj", cspr);
        }

        protected  void  csprojUser(String program){
            String cspUser="<?xml version=\"1.0\" encoding=\"utf-8\"?>\n" +
                    "<Project ToolsVersion=\"Current\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">\n" +
                    "  <PropertyGroup>\n" +
                    "    <UseIISExpress>true</UseIISExpress>\n" +
                    "    <Use64BitIISExpress />\n" +
                    "    <IISExpressSSLPort>44363</IISExpressSSLPort>\n" +
                    "    <IISExpressAnonymousAuthentication />\n" +
                    "    <IISExpressWindowsAuthentication />\n" +
                    "    <IISExpressUseClassicPipelineMode />\n" +
                    "    <UseGlobalApplicationHostFile />\n" +
                    "    <LastActiveSolutionConfig>Debug|Any CPU</LastActiveSolutionConfig>\n" +
                    "    <ActiveDebugTarget>ContainerToolsProjectDebugExtension</ActiveDebugTarget>\n" +
                    "  </PropertyGroup>\n" +
                    "  <ProjectExtensions>\n" +
                    "    <VisualStudio>\n" +
                    "      <FlavorProperties GUID=\"{"+typeUuid+"}\">\n" +
                    "        <WebProjectProperties>\n" +
                    "          <StartPageUrl>\n" +
                    "          </StartPageUrl>\n" +
                    "          <StartAction>CurrentPage</StartAction>\n" +
                    "          <AspNetDebugging>True</AspNetDebugging>\n" +
                    "          <SilverlightDebugging>False</SilverlightDebugging>\n" +
                    "          <NativeDebugging>False</NativeDebugging>\n" +
                    "          <SQLDebugging>False</SQLDebugging>\n" +
                    "          <ExternalProgram>\n" +
                    "          </ExternalProgram>\n" +
                    "          <StartExternalURL>\n" +
                    "          </StartExternalURL>\n" +
                    "          <StartCmdLineArguments>\n" +
                    "          </StartCmdLineArguments>\n" +
                    "          <StartWorkingDirectory>\n" +
                    "          </StartWorkingDirectory>\n" +
                    "          <EnableENC>True</EnableENC>\n" +
                    "          <AlwaysStartWebServerOnDebug>False</AlwaysStartWebServerOnDebug>\n" +
                    "        </WebProjectProperties>\n" +
                    "      </FlavorProperties>\n" +
                    "    </VisualStudio>\n" +
                    "  </ProjectExtensions>\n" +
                    "</Project>";
            files.put(program+".csproj.user", cspUser);
        }

        protected  void  webConfig(){
            String WebConfig="<?xml version=\"1.0\" encoding=\"utf-8\"?>\n" +
                    "\n" +
                    "<!--\n" +
                    "  有关如何配置 ASP.NET 应用程序的详细信息，请访问\n" +
                    "  https://go.microsoft.com/fwlink/?LinkId=169433\n" +
                    "-->\n" +
                    "<configuration>\n" +
                    "  <system.web>\n" +
                    "    <compilation debug=\"true\" targetFramework=\"4.7.2\"/>\n" +
                    "    <httpRuntime targetFramework=\"4.7.2\"/>\n" +
                    "  </system.web>\n" +
                    "  <system.codedom>\n" +
                    "    <compilers>\n" +
                    "      <compiler language=\"c#;cs;csharp\" extension=\".cs\"\n" +
                    "        type=\"Microsoft.CodeDom.Providers.DotNetCompilerPlatform.CSharpCodeProvider, Microsoft.CodeDom.Providers.DotNetCompilerPlatform, Version=2.0.1.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\"\n" +
                    "        warningLevel=\"4\" compilerOptions=\"/langversion:default /nowarn:1659;1699;1701\"/>\n" +
                    "      <compiler language=\"vb;vbs;visualbasic;vbscript\" extension=\".vb\"\n" +
                    "        type=\"Microsoft.CodeDom.Providers.DotNetCompilerPlatform.VBCodeProvider, Microsoft.CodeDom.Providers.DotNetCompilerPlatform, Version=2.0.1.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\"\n" +
                    "        warningLevel=\"4\" compilerOptions=\"/langversion:default /nowarn:41008 /define:_MYTYPE=\\&quot;Web\\&quot; /optionInfer+\"/>\n" +
                    "    </compilers>\n" +
                    "  </system.codedom>\n" +
                    "\n" +
                    "</configuration>\n";
            files.put("Web.config", WebConfig);
        }

        protected  void  webDebugConfig(){
            String WebDebugConfig="<?xml version=\"1.0\" encoding=\"utf-8\"?>\n" +
                    "\n" +
                    "<!-- 有关使用 web.config 转换的详细信息，请访问 https://go.microsoft.com/fwlink/?LinkId=125889 -->\n" +
                    "\n" +
                    "<configuration xmlns:xdt=\"http://schemas.microsoft.com/XML-Document-Transform\">\n" +
                    "  <!--\n" +
                    "    在下例中，“SetAttributes”转换将更改 \n" +
                    "    “connectionString”的值，以仅在“Match”定位器 \n" +
                    "    找到值为“MyDB”的特性“name”时使用“ReleaseSQLServer”。\n" +
                    "    \n" +
                    "    <connectionStrings>\n" +
                    "      <add name=\"MyDB\" \n" +
                    "        connectionString=\"Data Source=ReleaseSQLServer;Initial Catalog=MyReleaseDB;Integrated Security=True\" \n" +
                    "        xdt:Transform=\"SetAttributes\" xdt:Locator=\"Match(name)\"/>\n" +
                    "    </connectionStrings>\n" +
                    "  -->\n" +
                    "  <system.web>\n" +
                    "    <!--\n" +
                    "      \n" +
                    "      在下例中，“Replace”转换将替换 \n" +
                    "      web.config 文件的整个 <customErrors> 节。\n" +
                    "      请注意，由于 \n" +
                    "      在 <system.web> 节点下仅有一个 customErrors 节，因此不需要使用“xdt:Locator”特性。\n" +
                    "      \n" +
                    "      <customErrors defaultRedirect=\"GenericError.htm\"\n" +
                    "        mode=\"RemoteOnly\" xdt:Transform=\"Replace\">\n" +
                    "        <error statusCode=\"500\" redirect=\"InternalError.htm\"/>\n" +
                    "      </customErrors>\n" +
                    "    -->\n" +
                    "  </system.web>\n" +
                    "</configuration>";
            files.put("Web.debug.config", WebDebugConfig);
        }

        protected  void  webReleaseConfig(){
            String WebReleaseConfig="<?xml version=\"1.0\" encoding=\"utf-8\"?>\n" +
                    "\n" +
                    "<!-- 有关使用 web.config 转换的详细信息，请访问 https://go.microsoft.com/fwlink/?LinkId=125889 -->\n" +
                    "\n" +
                    "<configuration xmlns:xdt=\"http://schemas.microsoft.com/XML-Document-Transform\">\n" +
                    "  <!--\n" +
                    "    在下例中，“SetAttributes”转换将更改 \n" +
                    "    “connectionString”的值，以仅在“Match”定位器 \n" +
                    "    找到值为“MyDB”的特性“name”时使用“ReleaseSQLServer”。\n" +
                    "    \n" +
                    "    <connectionStrings>\n" +
                    "      <add name=\"MyDB\" \n" +
                    "        connectionString=\"Data Source=ReleaseSQLServer;Initial Catalog=MyReleaseDB;Integrated Security=True\" \n" +
                    "        xdt:Transform=\"SetAttributes\" xdt:Locator=\"Match(name)\"/>\n" +
                    "    </connectionStrings>\n" +
                    "  -->\n" +
                    "  <system.web>\n" +
                    "    <compilation xdt:Transform=\"RemoveAttributes(debug)\" />\n" +
                    "    <!--\n" +
                    "      \n" +
                    "      在下例中，“Replace”转换将替换 \n" +
                    "      web.config 文件的整个 <customErrors> 节。\n" +
                    "      请注意，由于 \n" +
                    "      在 <system.web> 节点下仅有一个 customErrors 节，因此不需要使用“xdt:Locator”特性。\n" +
                    "      \n" +
                    "      <customErrors defaultRedirect=\"GenericError.htm\"\n" +
                    "        mode=\"RemoteOnly\" xdt:Transform=\"Replace\">\n" +
                    "        <error statusCode=\"500\" redirect=\"InternalError.htm\"/>\n" +
                    "      </customErrors>\n" +
                    "    -->\n" +
                    "  </system.web>\n" +
                    "</configuration>";
            files.put("Web.release.config", WebReleaseConfig);
        }


    }
}
