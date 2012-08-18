var jHelper={
    checkValAttr: function(target) {
        $("select",target).each(function() {
            var v=$(this).attr("val");
            if(v==undefined) { v=""; }
            if(v!="") {
                if($(this).attr("multiple")!=undefined) {
                    var arr=v.split(",");
                    $.each(this.options,function(i,opt) {
                        $.each(arr,function(i,val) {
                            if(opt.value==val) {
                                opt.selected=true;
                            }
                        });
                    });
                } else {
                    this.value=v;
                }
            }
        });
        $(":input:checkbox",target).each(function() {
            var v=$(this).attr("val");
            if(v==undefined) { v=""; }
            if(v.toLowerCase()=="true") { this.checked=true; }
        });
    }
	,setDatePicker: function(target) {
	    $("[date-picker]",target)
		.each(function() {
		    var $txt=$(this);
		    var $inputAppend=$("<div class='input-append'><button class='btn'><i class='icon-calendar'>&nbsp;</i></button></div>");
		    $txt
            .before($inputAppend)
            .datepicker({ changeMonth: true,changeYear: true })
            ;
		    var $btn=$(".btn",$inputAppend);
		    $btn.before($txt);
		    $btn.click(function() { $txt.datepicker("show"); });
		});
	}
	,createBSDDL: function(target) {
	    $("select",target).each(function() {
	        if($(this).attr("allow_single_deselect")=="true") {
	            $(this).chosen({ allow_single_deselect: true });
	        } else {
	            $(this).chosen();
	        }
	    });
	}
    ,createFancyFile: function(target) {
        $(":input[type='file']",target).jqFile();
    }
	,setUpDialogBody: function(target) {
	    this.checkValAttr(target);
	    this.setDatePicker(target);
	    this.createBSDDL(target);
	    this.setUpAutoComplete(target);
	    this.resizeDialog();
	}
    ,setUpAutoComplete: function(target) {
        $("[dropdown-style='true']",target).each(function() {
            $box=$("<div class='input-append'></div>");
            $btn=$("<button class='btn' type='button'><i class='icon-chevron-down'>&nbsp;</i></button>");
            $(this).before($box);
            $box.append(this).append($btn);
        });
    }
    ,resizeDialog: function() {
        $(".ui-dialog-content:visible").each(function() {
            var dialog=$(this).data("dialog");
            dialog.option("position",dialog.options.position);
        });
    }
}
$(window).resize(function() {
    try {
        jHelper.resizeDialog();
    } catch(ex) { }
});