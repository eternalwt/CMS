Module.prototype.pageInit=function() {
    var that=this;
    _EditBox=$("#EditModule");
    _EditBox
	.bootdialog({
	    "backdrop": true
	})
	.bind('shown',function() {
	    $("#close",_EditBox)
		.unbind("click")
		.click(function() {
		    _EditBox.bootdialog("hide");
		});
	    _EditBox.bootdialog("resize");
	})
	;

    $("#frmEditModule").submit(function() {
        return false;
    });

    $("#new").click(function() {
        that.editModule(0);
    });

    var save=$("#save",_EditBox)
	.click(function() {
	    $(this).button("loading");
	    var frm=$("#frmEditModule");
	    that.Save(frm.serializeArray());
	});

    $("#selectall").click(function() {
        if(this.checked)
            $(":input[name='ModuleID']").attr("checked","checked");
        else
            $(":input[name='ModuleID']").removeAttr("checked");
    });

    $("#modulelist")
    .bootstrapTable({ url: "/Admin/Module/List"
		,onSuccess: function(t,data) {
		    $("tbody",t).remove();
		    $("#gridtemplate").tmpl(data).appendTo(t);
		    $("[title='Edit']",t).click(function() {
		        var tr=$(this).parents("tr:first");
		        var id=$("#ModuleID",tr).val();
		        that.editModule(id);
		    });
		}
		,paging: true
		,pagesize: 20
		,pageindex: 1
		,sortname: ""
		,sortorder: "asc"
    });

    $(window).resize(function() { _EditBox.bootdialog("resize"); });
};
Module.prototype.editModule=function(id) {
    _EditBox.bootdialog("show");
    var bootdialogbody=$(".bootdialog-body",_EditBox);
    bootdialogbody
	.html("<div class='loading'>Loading...</div>");
    this.Load(id);
};
var _EditBox;
var _Module=new Module();
_Module.onEditInfo=function(data) {
    var bootdialogbody=$(".bootdialog-body",_EditBox);
    bootdialogbody.empty();
    $("#edittemplate").tmpl(data).appendTo(bootdialogbody);
    $("#PositionName",bootdialogbody).autocomplete({
        appendTo: "body",source: "/Admin/Position/Search"
		,minLength: 0
		,comboboxstyle: true
		,select: function(event,ui) {
		    $("#PositionID",bootdialogbody).val(ui.item.id);
		}
    });
    $("#ModuleTypeName",bootdialogbody).autocomplete({
        appendTo: "body",source: "/Admin/ModuleType/Search"
		,minLength: 0
		,comboboxstyle: true
		,select: function(event,ui) {
		    $("#ModuleTypeID",bootdialogbody).val(ui.item.id);
		}
    });
    jHelper.setUpDialogBody(bootdialogbody);
    _EditBox.bootdialog("resize");
};
_Module.onSave=function(data) {
    $("#save").button("reset");
    if(data.IsSuccess==false) {
        var bootdialogbody=$(".bootdialog-body",_EditBox);
        $(".alert-error",bootdialogbody).remove();
        $("#errortemplate").tmpl(data).appendTo(bootdialogbody);
        _EditBox.bootdialog("resize");
    } else {
        _EditBox.bootdialog("hide");
        $("#modulelist").bootstrapTableReload();
    }
};
$(document).ready(function() {
    _Module.pageInit();
});
 

