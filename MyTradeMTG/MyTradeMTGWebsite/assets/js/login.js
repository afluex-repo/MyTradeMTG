/*
Author       : theme_ocean
Template Name: Icocoin - Bitcoin & Cryptocurrency ICO Landing Page HTML Template
Version      : 1.0
*/
(function($) {
	'use strict';
	
	jQuery(document).on('ready', function(){

$("#modal_trigger").leanModal({top : 100, overlay : 0.6, closeButton: ".modal_close" });

$(function () {
    $(".user_login").show();
    $(".social_login").hide();
    
		// Calling Login Form
		$("#login_form").click(function(){
			$(".social_login").hide();
			
			return true;
		});

		// Calling Register Form
		$("#register_form").click(function(){
			$(".social_login").hide();
			$(".user_register").show();
			$(".user_login").hide();
			$(".header_title").text('Register');
			return false;
		});

    // Calling Register Form
		$(".login_modal").click(function () {
		    $(".user_login").show();
		    $(".social_login").hide();
		    $(".user_register").hide();
		    $(".header_title").text('Login');
		    return true;
		});

		
		// Going back to Social Forms
		$(".back_btn").click(function(){
			$(".user_login").hide();
			$(".user_register").hide();
			$(".social_login").show();
			$(".header_title").text('Forget Password');
			return false;
		});

	})	
		
	}); 	
	
	
				
})(jQuery);


  

