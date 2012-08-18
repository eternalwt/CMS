Module.prototype.pageInit=function() {
	var that=this;
	_EditBox=$("#EditModule");

	$("[dialog-close='true']").click(function() {
		that.closeEditContent();
	});

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
};
Module.prototype.closeEditContent=function() {
	$("#PageContent").show();
	$("#EditModule").hide();
};
Module.prototype.editModule=function(id) {
	$("#PageContent").hide();
	$("#EditModule").show();
	var bootdialogbody=$(".bootdialog-body",_EditBox);
	bootdialogbody
	.html("<div class='loading'>Loading...</div>");
	this.Load(id);
};
Module.prototype.removeparameters=function(target) {
	var $parameters=$("#parameters",target);
	$parameters.empty();
}
Module.prototype.loadparameters=function(data,target) {
	_Module.removeparameters(target);
	var $parameters=$("#parameters",target);
	$("#parametertemplate").tmpl(data).appendTo($parameters);
	jHelper.setUpDialogBody($parameters);
}

var _Module=new Module();
var _EditBox;
var _FileManager=new FileManager();

_Module.onEditInfo=function(data) {
	var bootdialogbody=$(".bootdialog-body",_EditBox);
	bootdialogbody.empty();
	$("#edittemplate").tmpl(data).appendTo(bootdialogbody);
	jHelper.setUpDialogBody(bootdialogbody);
	_Module.loadparameters(data.Parameter,bootdialogbody);
	$("#AccessLevelID").change(function() {
		var selectRoles=$("#selectRoles",bootdialogbody);
		if(this.value=="2")
			selectRoles.show();
		else
			selectRoles.hide();
	});
	$("#ModuleTypeID").change(function() {
		_Module.removeparameters(bootdialogbody);
		if(this.value!="") {
			$.getJSON("/Admin/ModuleType/GetParameters/"+this.value,function(data) {
				_Module.loadparameters(data,bootdialogbody);
			});
		}
	});
};
_Module.onSave=function(data) {
	$("#save").button("reset");
	if(data.IsSuccess==false) {
		var bootdialogbody=$(".bootdialog-body",_EditBox);
		$(".alert-error",bootdialogbody).remove();
		$("#errortemplate").tmpl(data).appendTo(bootdialogbody);
	} else {
		_Module.closeEditContent();
		$("#modulelist").bootstrapTableReload();
	}
};


$(document).ready(function() {
	_Module.pageInit();
});