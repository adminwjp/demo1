
require(['jquery', 'bootstrap', 'modernizr', 'site', 'livequery', 'main', 'lodash'
, 'unobtrusive', 'store', 'sideNav', 'layer', 'tnlayer', 'validate', 'validate.unobtrusive',
  'blockUI', 'jqueryform', 'form', 'placeholder', 'tntipsy', 'histroy', 'lazyload'], function () {
      $(function () {
          $('input, textarea,textbox').placeholder({ customClass: 'my-placeholder' });
      });
      // 按钮选中的样式
      $("input.form-control").bind('focus blur', function () {
          $(this).parent(".form-group").toggleClass("active");
      });
      //懒加载
      $("img.lazy").livequery(function () {
          $("img.lazy").lazyload({ effect: "fadeIn", failurelimit: 20 });
      });

      //选中样式
      function checkedActive($that) {
          if ($that.prop("checked")) {
              $that.parent().parent().addClass("active")
          }
          else {
              $that.parent().parent().removeClass("active")
          }
      }
      //单选按钮
      $(function () {
          $(".tn-checkbox").change(function () {
              checkedActive($(this))
          });
      });

      /**
      * 获取当前页面上的所有附件的AttachmentId
      * @param {number[]} attachmentIdArray 其他AttachmentId集合
      * @param {string} hiddenInputId 最终保存AttachmentId集合的隐藏input的Id
      * @param {string[]} editorIds 当前页面所有Html编辑器的Id集合
      * @param {jquery} attachmentItems 当前页面附件集合 默认值为：$('#attachmentList .attachmentItem')
      * @param {jquery} featuredImage 标题图集合 默认值：$("[name='FeaturedImageAttachmentId']")
      */
      $.fn.getAttachmentIds = function (attachmentIdArray, hiddenInputId, editorIds, attachmentItems, featuredImage) {
          //从Html编辑器中获取

          //从上传插件中获取
          var attachmentIds = [];
          var $attachmentIds = (hiddenInputId != undefined && hiddenInputId.length > 0) ? $(hiddenInputId) : $('#AttachmentIds');

          if (!Array.isArray) {
              Array.isArray = function (arg) {
                  return Object.prototype.toString.call(arg) === '[object Array]';
              };
          }

          if (typeof (UE) != "undefined") {
              editorIds = editorIds || ['Body'];
              if (Array.isArray(editorIds) && editorIds.length > 0) {
                  for (var i = 0; i < editorIds.length; i++) {
                      var editorId = editorIds[i];
                      //编辑器 img
                      var img = $(UE.getEditor(editorId).getContent()).find("img")
                      $(img).each(function () {
                          var id = Number($(this).data('id'));
                          if (!isNaN(id)) {
                              attachmentIds.push(id);
                          }
                      });

                      //编辑器中上传的文件(创建时才会有这个)
                      var uploadedFile = window['#uploader-' + editorId + '_uploadfile'];
                      for (var i in uploadedFile) {
                          try {
                              var response = JSON.parse(uploadedFile[i].response);
                              var id = response.attachmentId;
                              if ((!isNaN(id))) {
                                  attachmentIds.push(id);
                              }
                          } catch (e) {
                              console.log(e);
                          }
                      }

                      //已经上传的附件列表
                      var $attachmentItems = $('#attachmentList-' + editorId + ' .attachmentItem');
                      $attachmentItems.each(function () {
                          var id = Number($(this).data('id'));
                          if (!isNaN(id)) {
                              attachmentIds.push(id);
                          }
                      });
                  }
              }
          }

          //已经上传的附件列表
          var $attachmentItems = attachmentItems || $('#attachmentList .attachmentItem');
          $attachmentItems.each(function () {
              var id = Number($(this).data('id'));
              if (!isNaN(id)) {
                  attachmentIds.push(id);
              }
          });

          //标题图
          var featuredImageAttachmentId = Number((featuredImage || $("[name='FeaturedImageAttachmentId']")).val());
          if (!isNaN(featuredImageAttachmentId) && featuredImageAttachmentId !== 0) {
              attachmentIds.push(featuredImageAttachmentId);
          }

          //其他AttachmentId集合
          if (Array.isArray(attachmentIdArray) && attachmentIdArray.length > 0) {
              for (var i = 0; i < attachmentIdArray.length; i++) {
                  var attachmentId = Number(attachmentIdArray[i]);
                  if (!isNaN(attachmentId)) {
                      attachmentIds.push(attachmentId);
                  } else {
                      throw attachmentIdArray[i] + ' is not a number!';
                  }
              }
          }
          $attachmentIds.val(attachmentIds.join(','));
      }

  })