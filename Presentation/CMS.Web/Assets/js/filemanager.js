function FileManager() {
    this.lastAction=null;
    this.ckeditorInstanceName="";
    this.siteURL="";
    this.fmbox;
    this.open=function() {
        this.fmbox=$("#FMDialog");
        var _fm=this;
        if(!this.fmbox.get(0)) {
            this.fmbox=$("<div id='FMDialog'></div>");
            $("body").append(this.fmbox);
            this.fmbox.dialog({
                title: "File Manager",
                autoOpen: true,
                width: 1000,
                modal: true,
                position: 'middle',
                autoResize: true
            });
        } else {
            _fm.fmbox.dialog("open");
        }
        _fm.fmbox.empty();
        var data={};
        $("#filemanagertemplate").tmpl(data).appendTo(_fm.fmbox);
        $("#addfiles",_fm.fmbox).click(function() {
            $fileslist=$("#FilesList");
            $adfiles=$("#AddFiles");
            $fileslist.hide();
            $adfiles.show();
        });
        $("#deletefiles",_fm.fmbox).click(function() {
            var arr=new Array();
            $(".thumbnail :input",_fm.fmbox).each(function() {
                if(this.checked) {
                    arr[arr.length]={
                        "name": "FileName",
                        "value": this.value
                    };
                }
            });
            if(arr.length>0) {
                if(confirm("Are you sure to want to delete this files")) {
                    $.ajax({
                        type: "POST",
                        url: "/Admin/File/DeleteFile",
                        data: arr,
                        cache: false,
                        success: function(data) {
                            $(".thumbnail :input",_fm.fmbox).each(function() {
                                if(this.checked) {
                                    $(this).parents("li:first").remove();
                                }
                            });
                        },
                        error: function(data) {
                        }
                    });
                }
            }
        });
        $adfiles=$("#AddFiles");
        $adfiles.filedrop({
            url: "/Admin/File/Upload",
            paramname: 'files',
            maxFiles: 5,
            beforeSend:function(){
                var params = new Array();
                params[params.length] = {
                    "name":"uploadpath", 
                    "value":$("#FileUploadPath").val()
                    };
                return params;
            },
            dragOver: function() {
                $adfiles.css('background','blue');
            },
            dragLeave: function() {
                $adfiles.css('background','gray');
            },
            drop: function() {
                $adfiles.css('background','gray');
            },
            afterAll: function() {
                $adfiles.html('The file(s) have been uploaded successfully!');
            },
            uploadFinished: function(i,file,response,time) {
                $adfiles.append('<li>'+file.name+'</li>');
            }
        });
        jHelper.resizeDialog();
        _fm.loadDirs();
    };
    this.loadFiles=function(that) {
        $(".sel-folder",this.fmbox).removeClass("sel-folder");
        $(that).addClass("sel-folder");
        var li=$(that).parents("li:first");
        var dirname=$("#dirname",li).val();
        this.getFiles(dirname);
    };
    this.loadDirs=function() {
        var _fm=this;
        var $fmbox=_fm.fmbox;
        var $directorylist=$("#DirectoryList",$fmbox);
        $.ajax({
            type: "GET",
            url: "/Admin/File/DirectoryList?directoryName=Gallery",
            dataType: "JSON",
            cache: false,
            success: function(data) {
                $directorylist.empty();
                $.each(data.dirs,function(i,dir) {
                    $("#dirlisttemplate").tmpl(dir).appendTo($directorylist);
                });
                jHelper.resizeDialog();
                _fm.getFiles("Files\\Gallery");
                $directorylist.treeview({
                    toggle: function() {
                        console.log("%s was toggled.",$(this).find(">span").text());
                    }
                });
            },
            error: function(data) {
            }
        });
    };
    this.refresh=function() {
        if(this.lastAction) {
            this.lastAction();
        }
    };
    this.getFiles=function(dirname) {
        var _fm=this;
        this.lastAction=function() {
            this.getFiles(dirname);
        };
        var url="/Admin/File/FilesList";
        var FilesList=$("#FilesList .files-list",this.fmbox);
        FilesList
        .empty()
        .html("<div class='loading'>Loading...</div>");

        var params=new Array();
        params[params.length]={
            "name": "directoryName",
            "value": dirname
        }
        $("#FileUploadPath").val(dirname);
        $(".dirpath").html(dirname);
        $.ajax({
            type: "GET",
            url: url,
            data: params,
            dataType: "JSON",
            cache: false,
            success: function(data) {
                FilesList
                .empty();
                var thumbnails=$("<ul class='thumbnails'></ul>");
                FilesList.append(thumbnails);
                $("#filelisttemplate").tmpl(data).appendTo(thumbnails);
                jHelper.resizeDialog();

                $("a",thumbnails).lightBox({
                    fixedNavigation: true,
                    imageLoading: _fm.siteURL+'/Assets/img/lightbox-ico-loading.gif',
                    imageBtnPrev: _fm.siteURL+'/Assets/img/lightbox-btn-prev.gif',
                    imageBtnNext: _fm.siteURL+'/Assets/img/lightbox-btn-next.gif',
                    imageBtnClose: _fm.siteURL+'/Assets/img/lightbox-btn-close.gif',
                    imageBlank: _fm.siteURL+'/Assets/img/lightbox-blank.gif'
                });


                $(".thumbnail",_fm.fmbox).contextMenu({
                    menu: 'filemanagermenu'
                },
                function(action,el,pos) {
                    switch(action) {
                        case "select":
                            var isimage=$("#isimage",el).val();
                            if(isimage=="true") {
                                var filepath=$("#filepath",el).val();
                                var value="<img src='"+filepath+"' style='float:left;' />";
                                FileManager.insertHTML(value);
                            } else {
                                alert("Please select any image");
                            }
                            break;
                        case "selectthumb":
                            var isimage=$("#isimage",el).val();
                            if(isimage=="true") {
                                var thumbfilepath=$("#thumbfilepath",el).val();
                                var value="<img src='"+thumbfilepath+"' style='float:left;' />";
                                FileManager.insertHTML(value);
                            } else {
                                alert("Please select any image");
                            }
                            break;
                        case "delete":
                            if(confirm("Are you sure you want to delete this file?")) {
                                var filename=$("#filename",el).val();
                                $.ajax({
                                    type: "GET",
                                    url: "/Admin/File/DeleteFile?fileName="+filename,
                                    dataType: "JSON",
                                    cache: false
                                });
                                var li=$(el).parents("li:first");
                                $(el).remove();
                                $(li).remove();
                            }
                            break;
                    }

                });

            }
        });
    };
    this.insertHTML=function(html) {
        try {
            var oEditor=window.opener.CKEDITOR.instances[FileManager.ckeditorInstanceName];
            if(oEditor.mode=='wysiwyg') {
                oEditor.insertHtml(html);
                window.close();
            }
            else {
                alert('You must be in WYSIWYG mode!');
            }
        } catch(ex) {
        //alert(ex);
        }
    };
	 
}

 