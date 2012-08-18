(function($) {
	$.fn.jqFile=function() {
		return this.each(function() {
			var $fle=$(this);
			if($fle.attr("type")=="file") {
				$fle.parent().css({
					"position": "relative"
				});
				$fle.css({
					"opacity": "0","position": "absolute","left": "0"
				});
				var $spnlable=$("<span>Select files...</span>");
				$spnlable.css({
					"position": "relative","top": "4px","pointer": "cursor","color": "blue"
				});
				$fle
                .before($spnlable)
                .change(function() {
                	alert(this.files);
                });
			}
		});
	};
	$.fn.jqBootStrapDropDown=function() {
		return this.each(function() {
			if($(this).attr("BSDropDown")!="true") {
				$(this).attr("BSDropDown","true");
				var $select=$(this);
				var html='<div class="btn-group">';
				html+='<button class="btn"></button>';
				html+='<button class="btn dropdown-toggle" data-toggle="dropdown">';
				html+='<span class="caret"></span>';
				html+='</button>';
				html+='<ul class="dropdown-menu">';
				html+='</ul>';
				html+='</div>';
				var $ddlbox=$(html);
				var $ul=$(".dropdown-menu",$ddlbox);
				var $btn=$(".btn:first",$ddlbox);
				var selectClass=$select.attr("class");
				var selectStyle=$select.attr("style");
				if(selectClass==undefined)
					selectClass="";
				if(selectStyle==undefined)
					selectStyle="";
				$btn
				.addClass(selectClass)
				.attr("style",selectStyle)
				.css("text-align","left")
				;
				var btnhtml="";
				/* Now we add the options */
				$('option',$select).each(function(i) {
					if(this.selected) {
						btnhtml=this.text;
					}
					var oLi=$('<li><a href="#" value="'+this.value+'">'+this.text+'</a></li>');
					$ul.append(oLi);
				});
				$btn.html(btnhtml);

				$("a",$ul).click(function() {
					$btn.html($(this).html());
					$select.val($(this).attr("value"));
					$select.trigger('change');
				});

				$select
				.before($ddlbox)
				.hide()
				;

			}
		});
	};
})(jQuery);