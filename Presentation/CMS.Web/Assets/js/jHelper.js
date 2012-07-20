var jHelper={
    checkValAttr: function(target) {
        $("select",target).each(function() {
            var v=$(this).attr("val");
            if(v==undefined) { v=""; }
            if(v!="") { this.value=v; }
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
            .zIndex(1050)
            .before($inputAppend);
		    var $btn=$(".btn",$inputAppend);
		    $btn.before($txt);
		    $btn.click(function() { $txt.datepicker("show"); });
		    $txt.attr("isclose","true").datepicker({ changeMonth: true,changeYear: true });
		});
	}
	,createBSDDL: function(target) {
	    $("select",target).jqBootStrapDropDown();
	}
	,setUpDialogBody: function(target) {
	    this.checkValAttr(target);
	    this.setDatePicker(target);
	    this.createBSDDL(target);
	}
}