var serverBudgget;

$(function () {

    //serverBudgget = "10.250.0.94";
    serverBudgget = "localhost";
    Application.init();
    

	
});

function handleAjaxMessages() {
    $(document).ajaxSuccess(function (event, request) {
        checkAndHandleMessageFromHeader(request);
    }).ajaxError(function (event, request) {
        displayMessage(request.responseText, "alert-danger");
    });
}

function checkAndHandleMessageFromHeader(request) {
    var msg = request.getResponseHeader('X-Message');
    if (msg) {
        displayMessage(msg, request.getResponseHeader('X-Message-Type'));
    }
}

function displayMessage(message, messageType) {
    $("#messagewrapper").html('<div class=\"alert ' + messageType.toLowerCase() + ' alert-dismissable\"><button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-hidden=\"true\">&times;</button><strong>' + message + '</strong> <a href=\"#\" class=\"alert-link\"> </a>.</div>');
    //  alert(message);
    $("#messagewrapper .messagebox").text(message);
    displayMessages();
}

function displayMessages() {
    if ($("#messagewrapper").children().length > 0) {
        $("#messagewrapper").show();
        $("#messagewrapper").focus();
        //TODO: focus não esta funcionando.
     //   $("#DivMenu").focus();
        $('body, html').animate({
            scrollTop: 0
        }, 600);

        $(document).click(function () {
            clearMessages();
        });
    }
    else {
        $("#messagewrapper").hide();
    }
}

function clearMessages() {
    $("#messagewrapper").fadeOut(500, function () {
        $("#messagewrapper").empty();
    });
}





var Application = function () {
	
	var validationRules = getValidationRules ();
	
	return { init: init, validationRules: validationRules };
	
	function init () {
	    handleAjaxMessages();
	    displayMessages();
		enableBackToTop ();
		enableLightbox ();
		enableCirque ();
		enableEnhancedAccordion ();


		$('.ui-tooltip').tooltip();
	    $('.ui-popover').popover();
    

	}

	function enableCirque () {
		if ($.fn.lightbox) {
			$('.ui-lightbox').lightbox ();
		}
	}

	function enableLightbox () {
		if ($.fn.cirque) {
			$('.ui-cirque').cirque ({  });
		}
	}

	function enableBackToTop () {
		var backToTop = $('<a>', { id: 'back-to-top', href: '#top' });
		var icon = $('<i>', { class: 'icon-chevron-up' });

		backToTop.appendTo ('body');
		icon.appendTo (backToTop);
		
	    backToTop.hide();

	    $(window).scroll(function () {
	        if ($(this).scrollTop() > 150) {
	            backToTop.fadeIn ();
	        } else {
	            backToTop.fadeOut ();
	        }
	    });

	    backToTop.click (function (e) {
	    	e.preventDefault ();

	        $('body, html').animate({
	            scrollTop: 0
	        }, 600);
	    });
	}
	
	function enableEnhancedAccordion () {
		$('.accordion-toggle').on('click', function (e) {			
	         $(e.target).parent ().parent ().parent ().addClass('open');
	    });
	
	    $('.accordion-toggle').on('click', function (e) {
	        $(this).parents ('.panel').siblings ().removeClass ('open');
	    });
	    
	}
	
	function getValidationRules () {
		var custom = {
	    	focusCleanup: false,
			
			wrapper: 'div',
			errorElement: 'span',
			
			highlight: function(element) {
				$(element).parents ('.form-group').removeClass ('success').addClass('error');
			},
			success: function(element) {
				$(element).parents ('.form-group').removeClass ('error').addClass('success');
				$(element).parents ('.form-group:not(:has(.clean))').find ('div:last').before ('<div class="clean"></div>');
			},
			errorPlacement: function(error, element) {
				error.prependTo(element.parents ('.form-group'));
			}
	    	
	    };
	    
	    return custom;
	}
	
}();