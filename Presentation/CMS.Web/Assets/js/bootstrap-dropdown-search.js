/* ============================================================
* bootstrap-dropdownsearch.js v2.0.4
* http://twitter.github.com/bootstrap/javascript.html#dropdownsearchs
* ============================================================
* Copyright 2012 Twitter, Inc.
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
* http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
* ============================================================ */


!function ($) {

	"use strict"; // jshint ;_;


	/* DROPDOWN CLASS DEFINITION
	* ========================= */

	var toggle = '[data-toggle="dropdown-search"]'
    , DropdownSearch = function (element) {
    	var $el = $(element).on('click.dropdownsearch.data-api', this.toggle)
    	$('html').on('click.dropdownsearch.data-api', function () {
    		$el.parent().removeClass('open')
    	})
    }

	DropdownSearch.prototype = {

		constructor: DropdownSearch

  , toggle: function (e) {
  	var $this = $(this)
        , $parent
        , selector
        , isActive

  	if ($this.is('.disabled, :disabled')) return

  	selector = $this.attr('data-target')

  	if (!selector) {
  		selector = $this.attr('href')
  		selector = selector && selector.replace(/.*(?=#[^\s]*$)/, '') //strip for ie7
  	}

  	$parent = $(selector)
  	$parent.length || ($parent = $this.parent())

  	isActive = $parent.hasClass('open')

  	clearMenus()

  	if (!isActive) $parent.toggleClass('open')

  	var ul = $(".dropdown-menu:first", $parent);
  	var isenable = ul.attr("isenable");
  	if (isenable == undefined || isenable != "true") {
  		var search = $(":input[name='search']", $parent);
  		if (search.get(0)) {
  			var remoteurl = search.attr("remote-url");
  			var lastval = "";
  			isenable = search.attr("isenable");
  			if (isenable == undefined || isenable != "true") {
  				search
		.attr("isenable", "true")
		.unbind("keyup")
		.keyup(function () {
			setTimeout(function () {
				if (lastValue != search.val()) {
					lastValue = search.val();
					loadOptions(ul, remoteurl, search.val());
				}
			});
		})
		.focus();
  				loadOptions(ul, remoteurl, "");
  			} else {
  				search.focus();
  			}
  		} else {
  			setEvents(ul);
  		}
  	}

  	return false
  }

	}

	var lastValue = "";

	function clearMenus() {
		$(toggle).parent().removeClass('open')
	}

	function setEvents(ul) {
		var ddlbox = ul.parents(".dropdown-search-group:first");
		var btn = $(".btn:first", ddlbox);
		var hdn = $("#" + ddlbox.attr("hidden-field-id"), ddlbox);
		$("a", ul)
		.unbind("click")
		.click(function () {
			var assignValue = $(this).attr("assign-value");
			var assignId = $(this).attr("assign-id");
			hdn.val(assignId);
			btn.html(assignValue);
		});
	}

	function loadOptions(ul, remoteurl, searchval) {
		ul.empty();
		var li = $("<li><a href='#'>Loading...</a></li>");
		$(ul).append(li);
		var params = new Array();
		params[params.length] = { "name": "q", "value": searchval };
		$.ajax({
			type: "GET",
			url: remoteurl,
			data: params,
			dataType: "JSON",
			cache: false,
			success: function (data) {
				ul.empty();
				$.each(data, function (i, item) {
					li = $("<li><a assign-value='" + item.value + "' assign-id='" + item.id + "' href='#'>" + item.label + "</a></li>");
					$(ul).append(li);
				});
				setEvents(ul);
			},
			error: function (data) {
			}
		});

	}


	/* DROPDOWN PLUGIN DEFINITION
	* ========================== */

	$.fn.dropdownsearch = function (option) {
		return this.each(function () {
			var $this = $(this)
        , data = $this.data('dropdownsearch')
			if (!data) $this.data('dropdownsearch', (data = new DropdownSearch(this)))
			if (typeof option == 'string') data[option].call($this)
		})
	}

	$.fn.dropdownsearch.Constructor = DropdownSearch


	/* APPLY TO STANDARD DROPDOWN ELEMENTS
	* =================================== */

	$(function () {
		$('html').on('click.dropdownsearch.data-api', clearMenus)
		$('body')
      .on('click.dropdownsearch', '.dropdownsearch form', function (e) { e.stopPropagation() })
      .on('click.dropdownsearch.data-api', toggle, DropdownSearch.prototype.toggle)
	})

} (window.jQuery);