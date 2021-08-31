/**
 * @interface FileDetail
 * @member {string} id 文件id 该值应该与在上传列表中的键名相同
 * @member {string} name 文件名
 * @member {number?} size 大小(字节) 为空时不显示字节数
 * @member {number?} percent 上传进度百分比 为空时不显示该文件进度条
 * @member {number?} state 上传状态 1：等待上传 2：上传中 4：上传失败 5：已上传 为空时认为已上传
 * extend
 * @member {string?} response 服务器响应信息
 * @member {string?} url 文件地址 渲染预览图会使用这个地址
 * @member {string?} err 上传失败信息
 * @member {boolean?} selected 是否被选中
 */
/**
 * 通过window[exportvalue]访问上传列表对象，可通过操作这个对象来添加已上传文件
 * window[exportvalue] = { id1: FileDetail, id2: FileDetail, ... }
 * exportvalue默认为'#uploader-' + uploaderid
 */
+ function (factory) {
    if (typeof define === "function" && define.amd) {
        require(['plupload', 'moxie'], function (plupload, moxie) {
            window.plupload = plupload;
            window.moxie = moxie;
        });
        require(['jquery', 'plupload', 'plupload.zh-CN', 'livequery'], factory);
    } else if (typeof module === "object" && module.exports) {
        var plupload = require('plupload');
        window.plupload = plupload;
        module.exports = factory(require('jquery'), plupload, require('plupload.zh-CN'), require('livequery'));
    } else {
        factory(jQuery, plupload);
    }
}(function ($, plupload) {
    /**
     * 渲染进度条
     * @param {FileDetail} fileDetail 上传文件列表对象
     * @param {object} option 渲染选项
     * @param {boolean} [option.preview = false] 是否显示预览图
     * @param {boolean} [option.deleteIcon = true] 是否显示删除图标
     * @param {string?} [option.showProgress] 进度条显示选项 'true'总是显示 undefined隐藏上传完毕项的进度条
     * @returns {string} 渲染结果的html字符串
     */
    function renderProgress(fileDetail, option) {
        var opt = {
            uploadingOnly: false,
            placeholder: '',
            preview: false,
            deleteIcon: true,
            showProgress: undefined,
        };
        $.extend(opt, option);
        var keys = [];
        var template = '';
        for (var k in fileDetail) keys.push(k);
        keys.sort();
        if (!keys.length) {
            if (opt.placeholder) return '<div>' + opt.placeholder + '</div>';
            return '';
        }
        for (var i = 0; i < keys.length; i++) {
            var file = fileDetail[keys[i]];
            if (opt.uploadingOnly && file.state !== 2 && file.state !== 1) continue;
            var resp;
            if (file.response) resp = JSON.parse(file.response);
            var tmpl = '<div id="' + file.id + '"';
            if (file.response) tmpl += ' data-response="' + file.response.replace(/"/g, '&quot;') + '"';
            tmpl += ' data-uploader-item ';
            if (file.state === 5) {
                if (file.selected) {
                    tmpl += 'data-selected="true"'
                }
                if (file.selected === false) {
                    tmpl += 'data-selected="false"'
                }
            }
            tmpl += '>';
            var fileName = file.name;
            if (fileName.length > 30) {
                fileName = file.name.substring(0, 30) + "...";
            }
            //这里图片预览的时候不需要
            if (opt.preview) tmpl += '<img src="' + file.url + '" class="uploader-preview-img" />';
            //if (opt.preview) tmpl += '<img src="" class="uploader-preview-img" />';
            tmpl += '<span title="' + file.name + '">' + fileName + ' <span style="margin-left:15px;color:#999">';
            if (file.size) tmpl += plupload.formatSize(file.size); // 文件大小显示
            tmpl += '</span></span>';
            if (opt.deleteIcon && (file.state === 1 || file.state === 4)) tmpl += '<i class="fa fa-times" data-file-id="' + file.id + '"></i>'; // 删除图标显示
            tmpl += '<b>';
            // 进度条显示
            if (resp && resp.MessageType === -1) {
                tmpl += '<p style="color:red">' + resp.MessageContent + '</p>'
            } else if (file.state === 5 && opt.showProgress === undefined) { /* noop */ }
            else if (file.state === 4) {
                tmpl += '<p style="color:red">' + (file.err || '上传失败') + '</p>'
            } else if (file.percent !== undefined && file.state !== undefined) {
                tmpl += '<div class="progress" style="height:20px;"><div class="progress-bar';
                if (file.state === 2) tmpl += ' progress-bar-info';
                if (file.state === 5) tmpl += ' progress-bar-success';
                tmpl += '" role="progressbar" aria-valuenow="' + file.percent + '" aria-valuemin="0" aria-valuemax="100" style="min-width:8em;width:' + file.percent + '%"><span>';
                if (file.state === 1) tmpl += '等待上传';
                else if (file.state === 5) tmpl += '上传完毕';
                else tmpl += '上传中 ' + file.percent + '%';
                tmpl += '</span></div></div>';
            }
            tmpl += '</b></div>';
            template += tmpl;
        }
        return template;
    }
    /**
     * 提取file信息为FileDetail所需格式
     * @param {object} file plupload.file
     * @return {FileDetail}
     */
    function resolveFileDetail(file) {
        return {
            id: file.id,
            name: file.name,
            size: file.size,
            percent: file.percent,
            state: file.state,
        };
    }
    /**
     * 注册选择事件，在样式上只会改变列表项的data-selected属性值
     * 只有已上传的文件会响应该事件
     * @param {string} exportValue 上传列表对象名
     * @param {jquery} $progress 上传列表jqueryDOM对象
     * @param {boolean} reset 是否重置selected状态
     */
    function registerSelectEvent(exportValue, $progress, reset) {
        if (reset) {
            for (var key in window[exportValue]) {
                var fd = window[exportValue][key];
                fd.selected = false;
            }
        }
        $progress.find('[data-uploader-item]').off('click').click(function (e) {
            var $this = $(this);
            var fd = window[exportValue][$this.attr('id')];
            if (fd.state !== undefined && fd.state !== 5) return;
            fd.selected = !fd.selected;

            //为了解决IE8下伪元素动态作用样式不重绘的bug，参见：http://www.cnblogs.com/AndyWithPassion/p/IE8_pseudo_element_not_redraw.html
            $this.addClass('content-empty');
            setTimeout(function(){
                $this.removeClass('content-empty');
            }, 0);

            $this.attr('data-selected', !!fd.selected);
            $this.toggleClass('file-loaded-selected', !!fd.selected);
        });
        $progress.find('[data-uploader-item]').each(function () {
            var $this = $(this);
            var fd = window[exportValue][$this.attr('id')];
            if (fd.state !== undefined && fd.state !== 5) return;
            $this.attr('data-selected', !!fd.selected);
            $this.toggleClass('file-loaded-selected', !!fd.selected);
        })
    }
    $('[data-plugin="fileuploader"]').livequery(function () {
        var $this = $(this);
        var $parent = $this.parent();
        var pickfileId = $this.data('pickfile-id'); // 选择文件按钮id
        var uploadfileId = $this.data('uploadfile-id'); // 上传文件按钮id
        var progressContainerId = $this.data('progress-id') || $this.attr('id') + '_progress'; // 进度条容器id
        var showProgress = $this.data('show-progress'); // {string?} 是否显示上传列表进度条 'true'总是显示进度条 'false'不显示 默认显示并隐藏上传完毕项的进度条
        var maxcount = +$this.data('maxcount') || 0; // plupload未实现该功能 这里为0时为单选，其余为多选
        var multiple = ('' + $this.data('multiple')) !== 'false'; // [true]
        var autoUpload = ('' + $this.data('auto-upload')) !== 'false'; // {boolean}=true 选择完毕后自动上传
        var uploadurl = $this.data('uploadurl'); // 上传url
        var ownerid = $this.data('ownerid');
        var tenanttypeid = $this.data('tenanttypeid');
        var associateid = $this.data('associateid');
        var extensions = $this.data('extensions') || '*'; // {string}='*' 接受的扩展名,用','隔开
        var preview = !!$this.data('preview'); // [false] 图片显示预览图
        var selectable = $this.data('selectable');
        var innercontent = $this.data('innercontent');
        var uploadingOnly = !!$this.data('uploading-only'); // [false] 上传列表中只显示上传中的项目

        var exportValue = $this.data('exportvalue') || ('#uploader-' + $this.attr('id')); // {string->JSON.stringify(FileDetail[])} 将文件列表导出到全局变量

        var onUploadComplete = $this.data('onupload') || $this.data('uploadsuccess'); // 上传成功回调函数名 (file, JSONresp, uploader) |> cb 每个文件上传成功都会触发
        var onUploadError = $this.data('onerror'); // 上传失败回调函数名 (up, err) |> cb
        var onUploadStart = $this.data('onstart'); // 开始上传回调函数名 (uploader, files) |> cb

        var progressInited = false;
        if (innercontent) $this.html('<div>' + innercontent + '</div>');
        var renderOption = {
            uploadingOnly: uploadingOnly,
            placeholder: innercontent,
            preview: preview,
            deleteIcon: !autoUpload,
            showProgress: showProgress,
        };
        var $progress = $('<div id="' + progressContainerId + '" class="upload-progress-container' + (preview ? ' upload-preview' : '') + '" style="display:none;" data-export="' + exportValue + '"></div>');
        if (typeof window[exportValue] !== 'object') window[exportValue] = {};

        window[exportValue + '.forceUpdate'] = function () {
            $progress.html(renderProgress(window[exportValue], renderOption));
            selectable && registerSelectEvent(exportValue, $progress, true);
        }

        // if (progressContainerId && !preview) $('#' + progressContainerId).empty().append($progress);
        // if ($this.data('progress-id') === undefined) {
        //     $('#' + $this.attr('id')).empty().append($progress);
        // } else {
        //     $('#' + progressContainerId).empty().append($progress);
        // }
        progressInited = true;
        // else $this.before($progress);
        if (showProgress !== 'false') $progress.css({ display: '' });
        var uploader = new plupload.Uploader({
            runtimes: 'html5,flash,silverlight,html4',

            browse_button: pickfileId || $this[0],
            url: uploadurl,

            // 是否允许多选
            multi_selection: !!+maxcount,

            filters: {
                // max_file_size: '10mb',
                mime_types: [{
                    title: "请选择文件",
                    extensions: extensions
                }]
            },
            multipart: multiple,
            multipart_params: {
                encode: 'utf-8',
                ownerId: ownerid,
                tenantTypeId: tenanttypeid,
                associateId: associateid,
                btnSelector: $this.attr('id')
            },
            flash_swf_url: '/plupload/js/Moxie.swf',
            silverlight_xap_url: '/plupload/js/Moxie.xap',

            init: {
                // 初始化完毕回调
                PostInit: function () {
                    if (uploadfileId) {
                        $('#' + uploadfileId).click(function () {
                            uploader.start();
                        });
                    }
                    //20180716 liqg 修改当文件上传结束时还原进度条中的文字 可能导致指定进度条容器时会有显示bug 但是我们一般不单独指定进度条容器
                    if (!$this.data('isueditor')) {
                        window[exportValue] = {};
                    }
                    if ($this.data('progress-id') === undefined) {
                        $('#' + $this.attr('id')).empty().append($progress);
                    } else {
                        $('#' + progressContainerId).empty().append($progress);
                    }
                    $progress.html(renderProgress(window[exportValue], renderOption));
                    selectable && registerSelectEvent(exportValue, $progress, true);
                },
                // 添加文件回调
                FilesAdded: function (up, files) {
                    !progressInited && $('#' + progressContainerId).empty().append($progress);
                    progressInited = true;
                    if (maxcount === 0) {
                        for (var i = 0; i < up.files.length; i++) {
                            if (up.files[i].id !== files[0].id) up.removeFile(up.files[i]);
                        }
                        window[exportValue] = {};
                    }
                    plupload.each(files, function (file) {
                        window[exportValue][file.id] = resolveFileDetail(file);
                        // 图片预览
                        if (preview && /image/.test(file.type)) {
                            try {
                                var fr = new window.moxie.file.FileReader();
                                fr.onload = function () {
                                    var dataurl = fr.result;
                                    fr.destroy();
                                    fr = null;
                                    if (dataurl.length < 52428800/* 50M */) window[exportValue][file.id].url = dataurl;
                                    $progress.html(renderProgress(window[exportValue], renderOption));
                                    $progress.find('i.fa-times').off('click').click(function (e) {
                                        e.stopPropagation();
                                        $deleteFile = $(this);
                                        uploader.removeFile($deleteFile.data('file-id'))
                                    });
                                }
                                fr.readAsDataURL(file.getSource());
                            } catch (e) {/* ie8 not support image preview */ window[exportValue][file.id].url = 'error'; }
                        } else {
                            $progress.html(renderProgress(window[exportValue], renderOption));
                            $progress.find('i.fa-times').off('click').click(function (e) {
                                e.stopPropagation();
                                $deleteFile = $(this);
                                uploader.removeFile($deleteFile.data('file-id'))
                            });
                        }
                    });
                    if (autoUpload) uploader.start();
                },
                FilesRemoved: function (up, files) {
                    for (file in files) {
                        delete window[exportValue][files[file].id];
                    }
                    $progress.html(renderProgress(window[exportValue], renderOption));
                    selectable && registerSelectEvent(exportValue, $progress);
                    $progress.find('i.fa-times').off('click').click(function (e) {
                        e.stopPropagation();
                        $deleteFile = $(this);
                        uploader.removeFile($deleteFile.data('file-id'))
                    });
                },
                BeforeUpload: function (file) {
                    $progress.find('.fa-times[data-file-id]').remove();
                    if (onUploadStart) eval('$.fn.' + onUploadStart + '||' + onUploadStart)(uploader, file);
                },
                // 进度条更新回调
                UploadProgress: function (up, file) {
                    if (showProgress === 'false') return;
                    window[exportValue][file.id].percent = file.percent;
                    window[exportValue][file.id].state = file.state;
                    $progress.html(renderProgress(window[exportValue], renderOption));
                },
                FileUploaded: function (uploader, file, result) {
                    window[exportValue][file.id] = resolveFileDetail(file);
                    var fd = window[exportValue][file.id];
                    fd.selected = true;
                    fd.response = result.response;
                    var resp = JSON.parse(result.response);
                    if (preview) fd.url = resp.url || resp.path;
                    file.path = fd.url;
                    file.id = resp.attachmentId || file.id;
                    file.name = resp.name || resp.Name;
                    var checkExt = /\.([^.]+)$/.exec(fd.name) || '';
                    if (checkExt[1]) checkExt = checkExt[1];
                    file.ext = checkExt;
                    $progress.html(renderProgress(window[exportValue], renderOption));
                    selectable && registerSelectEvent(exportValue, $progress);
                    //20180716 liqg 修改当文件上传结束时还原进度条中的文字 可能导致指定进度条容器时会有显示bug 但是我们一般不单独指定进度条容器
                    $('#' + progressContainerId).html(innercontent);
                    if (onUploadComplete) eval('$.fn.' + onUploadComplete + '||' + onUploadComplete)(file, resp, uploader);
                },
                Error: function (up, err) {
                    window[exportValue][err.file.id] = resolveFileDetail(err.file);
                    window[exportValue][err.file.id].err = err.message;
                    $progress.html(renderProgress(window[exportValue], renderOption));
                    selectable && registerSelectEvent(exportValue, $progress);
                    $progress.find('i.fa-times').off('click').click(function (e) {
                        e.stopPropagation();
                        $deleteFile = $(this);
                        uploader.removeFile($deleteFile.data('file-id'))
                    });
                    //20180716 liqg 修改当文件上传结束时还原进度条中的文字 可能导致指定进度条容器时会有显示bug 但是我们一般不单独指定进度条容器
                    $('#' + progressContainerId).html(innercontent);
                    if (onUploadError) eval('$.fn.' + onUploadError + '||' + onUploadError)(uploader, err.file);
                }
            }
        });
        uploader.init();
    });
});