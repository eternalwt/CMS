!function ($) {

	"use strict"

	/* CSS TRANSITION SUPPORT (https://gist.github.com/373874)
	* ======================================================= */

	var transitionEnd

	$(document).ready(function () {

		$.support.transition=(function () {
			var thisBody=document.body||document.documentElement
        ,thisStyle=thisBody.style
        ,support=thisStyle.transition!==undefined||thisStyle.WebkitTransition!==undefined||thisStyle.MozTransition!==undefined||thisStyle.MsTransition!==undefined||thisStyle.OTransition!==undefined
			return support
		})()

		// set CSS transition event type
		if($.support.transition) {
			transitionEnd="TransitionEnd"
			if($.browser.webkit) {
				transitionEnd="webkitTransitionEnd"
			} else if($.browser.mozilla) {
				transitionEnd="transitionend"
			} else if($.browser.opera) {
				transitionEnd="oTransitionEnd"
			}
		}

	})


	/* MODAL PUBLIC CLASS DEFINITION
	* ============================= */

	var BootDialog=function (content,options) {
		this.settings=$.extend({},$.fn.bootdialog.defaults,options)
		this.$element=$(content)
      .delegate('[data-dismiss="bootdialog"]','click.bootdialog',$.proxy(this.hide,this))

		if(this.settings.show) {
			this.show()
		}

		return this
	}

	BootDialog.prototype={

		toggle: function () {
			return this[!this.isShown?'show':'hide']()
		}

    ,show: function () {
    	var that=this
    	this.isShown=true
    	this.$element.trigger('show')

    	this.$element.css({ position: 'absolute',
    		left: ($(window).width()-this.$element.outerWidth())/2,
    		top: ($(window).height()-this.$element.outerHeight())/2
    	})


    	escape.call(this)
    	backdrop.call(this,function () {
    		var transition=$.support.transition&&that.$element.hasClass('fade')

    		that.$element
            .appendTo(document.body)
            .show()

    		if(transition) {
    			that.$element[0].offsetWidth // force reflow
    		}

    		that.$element.addClass('in')

    		transition?
            that.$element.one(transitionEnd,function () { that.$element.trigger('shown') }):
            that.$element.trigger('shown')

    		that.$element.trigger('resize');

    	})

    	return this
    }

    ,hide: function (e) {
    	e&&e.preventDefault()

    	if(!this.isShown) {
    		return this
    	}

    	var that=this
    	this.isShown=false

    	escape.call(this)

    	this.$element
          .trigger('hide')
          .removeClass('in')

    	$.support.transition&&this.$element.hasClass('fade')?
          hideWithTransition.call(this):
          hideBootDialog.call(this)

    	return this
    }
	,resize: function () {
		this.$element
		.css({ position: 'absolute',
			left: ($(window).width()-this.$element.outerWidth())/2,
			top: ($(window).height()-this.$element.outerHeight())/2
		})
	}

	}


	/* MODAL PRIVATE METHODS
	* ===================== */

	function hideWithTransition() {
		// firefox drops transitionEnd events :{o
		var that=this
      ,timeout=setTimeout(function () {
      	that.$element.unbind(transitionEnd)
      	hideBootDialog.call(that)
      },500)

		this.$element.one(transitionEnd,function () {
			clearTimeout(timeout)
			hideBootDialog.call(that)
		})
	}

	function hideBootDialog(that) {
		this.$element
      .hide()
      .trigger('hidden')

		backdrop.call(this)
	}

	function backdrop(callback) {
		var that=this
      ,animate=this.$element.hasClass('fade')?'fade':''
		if(this.isShown&&this.settings.backdrop) {
			var doAnimate=$.support.transition&&animate

			this.$backdrop=$('<div class="bootdialog-backdrop '+animate+'" />')
        .appendTo(document.body)

			if(this.settings.backdrop!='static') {
				this.$backdrop.click($.proxy(this.hide,this))
			}

			if(doAnimate) {
				this.$backdrop[0].offsetWidth // force reflow
			}

			this.$backdrop.addClass('in')

			doAnimate?
        this.$backdrop.one(transitionEnd,callback):
        callback()

		} else if(!this.isShown&&this.$backdrop) {
			this.$backdrop.removeClass('in')

			$.support.transition&&this.$element.hasClass('fade')?
        this.$backdrop.one(transitionEnd,$.proxy(removeBackdrop,this)):
        removeBackdrop.call(this)

		} else if(callback) {
			callback()
		}
	}

	function removeBackdrop() {
		this.$backdrop.remove()
		this.$backdrop=null
	}

	function escape() {
		var that=this
		if(this.isShown&&this.settings.keyboard) {
			$(document).bind('keyup.bootdialog',function (e) {
				if(e.which==27) {
					that.hide()
				}
			})
		} else if(!this.isShown) {
			$(document).unbind('keyup.bootdialog')
		}
	}


	/* MODAL PLUGIN DEFINITION
	* ======================= */

	$.fn.bootdialog=function (options) {
		var bootdialog=this.data('bootdialog')

		if(!bootdialog) {

			if(typeof options=='string') {
				options={
					show: /show|toggle/.test(options)
				}
			}

			return this.each(function () {
				$(this).data('bootdialog',new BootDialog(this,options))
			})
		}

		if(options===true) {
			return bootdialog
		}

		if(typeof options=='string') {
			bootdialog[options]()
		} else if(bootdialog) {
			bootdialog.toggle()
		}

		return this
	}

	$.fn.bootdialog.BootDialog=BootDialog

	$.fn.bootdialog.defaults={
		backdrop: true
  ,keyboard: true
  ,show: false
	}


	/* MODAL DATA- IMPLEMENTATION
	* ========================== */

	$(document).ready(function () {
		$('body').delegate('[data-controls-bootdialog]','click',function (e) {
			e.preventDefault()
			var $this=$(this).data('show',true)
			$('#'+$this.attr('data-controls-bootdialog')).bootdialog($this.data())
		})
	})

} (window.jQuery||window.ender);
