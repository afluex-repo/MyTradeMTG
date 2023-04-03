document.write(`
    
<!-- START FOOTER -->
		<div class="footer">
			<div class="container">		
				<div class="row text-center">						
					<div class ="col-lg-12 col-sm-12 col-xs-12">
						<div class="footer_profile">
						<img src="asset/img/MtgCoinPng.png" class ="img-fluid mb-1" style="height:5rem;" />
                        <h2 class="text-dark mb-4 mtg_footer">MTG Coin</h2>
							<ul>
								<li><a href="#"><i class="fa fa-facebook"></i></a></li>
								<li><a href="#"><i class="fa fa-twitter"></i></a></li>
								<li><a href="#"><i class="fa fa-instagram"></i></a></li>
								<li><a href="#"><i class="fa fa-linkedin"></i></a></li>
								<li><a href="#"><i class="fa fa-youtube"></i></a></li>
							</ul>
						</div>
						<div class="footer_copyright">
							<p>&copy; 2022. All Rights Reserved by My Trade</p>
						</div>						
					</div><!--- END COL -->							
				</div><!--- END ROW -->					
			</div><!--- END CONTAINER -->
		</div>
		<!--END FOOTER-->
        <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.3/jquery.min.js"></script>
        			<script src="asset/js/jquery-1.12.4.min.js"></script>
		<!--Latest compiled and minified Bootstrap-->
			<script src="asset/bootstrap/js/bootstrap.min.js"></script>
		<!--owl-carousel min js-->
			<script src="asset/owlcarousel/js/owl.carousel.min.js"></script>
		<!--jquery counterup-->
			<script src="asset/js/jquery.counterup.min.js"></script>
			<script src="asset/js/countdown.js"></script>
		<!--jquery.slicknav-->
			<script src="asset/js/jquery.slicknav.js"></script>
		<!--particles-->
			<script src="asset/js/particles.min.js"></script>
			<script src="asset/js/app.js"></script>
		<!--jquery.smooth-scroll-->
			<script src="asset/js/smooth-scroll.js"></script>
		<!--magnific-popup js-->
			<script src="asset/js/jquery.magnific-popup.min.js"></script>
		<!--scrolltopcontrol js-->
			<script src="asset/js/scrolltopcontrol.js"></script>
		<!--WOW -Reveal Animations When You Scroll-->
			<script src="asset/js/wow.min.js"></script>

		<!--scripts js-->
			<script src="asset/js/main.js"></script>
			<script src="asset/js/trading-widget.min.js"></script>
			<script src="asset/js/utilities.min.js"></script>
            <script src="asset/js/config-theme.js"></script>
		<!--jquery leanModal min js-->
			<script src="asset/js/jquery.leanModal.min.js"></script>
		<!--login js-->
			<script src="asset/js/login.js"></script>
		<!--scripts js-->
			<script src="asset/js/main.js"></script>

	<script src="https://cdnjs.cloudflare.com/ajax/libs/intl-tel-input/11.0.9/js/intlTelInput.js"></script>
	<script src="https://cdnjs.cloudflare.com/ajax/libs/intl-tel-input/11.0.9/js/intlTelInput.min.js"></script>
	<script src="https://cdnjs.cloudflare.com/ajax/libs/intl-tel-input/11.0.9/js/utils.js"></script>
    <script>
    $(document).ready(function () {
    debugger;
    $("#my").modal('show');
});
    </script>
            <script>
            $('#modal').hide();
            $('#modal_trigger').click(function()
                {
            $('#modal').toggle();
             })
             $('#modal_sign').click(function()
            {         
                 $('#modal').toggle();
                })
                 $('.modal_close').click(function()
                {

                $('#modal').hide();
                })
       </script>
			<script>

var telInput = $("#phone"),
  errorMsg = $("#error-msg"),
  validMsg = $("#valid-msg");

// initialise plugin
telInput.intlTelInput({

    allowExtensions: true,
    formatOnDisplay: true,
    autoFormat: true,
    autoHideDialCode: true,
    autoPlaceholder: true,
    defaultCountry: "auto",
    ipinfoToken: "yolo",

    nationalMode: false,
    numberType: "MOBILE",
  //onlyCountries: ['us', 'gb', 'ch', 'ca', 'do'],
    preferredCountries: ['sa', 'ae', 'qa', 'om', 'bh', 'kw', 'ma'],
    preventInvalidNumbers: true,
    separateDialCode: true,
    initialCountry: "auto",
    geoIpLookup: function(callback) {
  $.get("http://ipinfo.io", function() { }, "jsonp").always(function(resp) {
    var countryCode = (resp && resp.country) ? resp.country: "";
    callback(countryCode);
});
},
    utilsScript: "https://cdnjs.cloudflare.com/ajax/libs/intl-tel-input/11.0.9/js/utils.js"
});

var reset = function() {
  telInput.removeClass("error");
  errorMsg.addClass("hide");
  validMsg.addClass("hide");
};

// on blur: validate
telInput.blur(function() {
  reset();
  if ($.trim(telInput.val())) {
    if (telInput.intlTelInput("isValidNumber")) {
      validMsg.removeClass("hide");
} else {
      telInput.addClass("error");
      errorMsg.removeClass("hide");
}
}
});

// on keyup / change flag: reset
telInput.on("keyup change", reset);

 

            </script>

             <script>
       
    </script>
`);