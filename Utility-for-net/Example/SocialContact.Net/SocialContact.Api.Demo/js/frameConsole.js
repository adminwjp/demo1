
require(['jquery', 'bootstrap', 'modernizr', 'site', 'livequery', 'main', 'lodash', 'unobtrusive', 'store', 'sideNav', 'layer', 'tnlayer', 'validate', 'validate.unobtrusive', 'blockUI', 'jqueryform', 'form', 'placeholder'], function () {

    $('.maintenance').on('click', function () {
        layer.alert('您没有权限查看，查看请联系管理员。', {
            icon: 2,
            shadeClose: true
        })
    });

    $('input, textarea').livequery(function () {
        $('input, textarea').placeholder({ customClass: 'my-placeholder' });
    });

    //选中样式
    $.fn.checkedActive = function ($that) {
        if ($that.prop("checked")) {
            $that.parent().parent().addClass("active")
        }
        else {
            $that.parent().parent().removeClass("active")
        }
        return this
    }

    ////单选按钮
    //$(function () {
    //    $(".tn-checkbox").change(function () {
    //        $.fn.checkedActive($(this))
    //    });
    //});

    $.fn.checkActive = function () {
        $(".tn-checkbox").change(function () {
            $.fn.checkedActive($(this))
        });
    }


    /**
     * 获取当前页面上的所有附件的AttachmentId
     * @param {number[]} attachmentIdArray 其他AttachmentId集合
     * @param {jquery} hiddenInput 最终保存AttachmentId集合的隐藏input
     * @param {string[]} editorIds 当前页面所有Html编辑器的Id集合
     * @param {jquery} attachmentItems 当前页面附件集合 默认值为：$('#attachmentList .attachmentItem')
     * @param {jquery} featuredImage 标题图集合 默认值：$("[name='FeaturedImageAttachmentId']")
     */
    $.fn.getAttachmentIds = function (attachmentIdArray, hiddenInput, editorIds, attachmentItems, featuredImage) {
        //从Html编辑器中获取
        //从上传插件中获取
        var attachmentIds = [];
        var $attachmentIds = hiddenInput || $('#AttachmentIds');

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