document.write(`
<!-- Meta -->
		<meta charset="utf-8">
		<meta http-equiv="X-UA-Compatible" content="IE=edge">
		<meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
		<meta name="description" content="">
		<meta name="keywords" content="">
		<!-- SITE TITLE -->
		<title>My Trade</title>
		<!-- Latest Bootstrap min CSS -->
		<link rel="stylesheet" href="assets/bootstrap/css/bootstrap.min.css">
		<!-- Google Font -->
		<link href="https://fonts.googleapis.com/css2?family=Montserrat:wght@300;400;500;600;700;800;900&amp;display=swap" rel="stylesheet">
		<link rel="icon" type="image/png" href="assets/img/MtgCoinPng.png">
		<!-- Font Awesome CSS -->
		<link rel="stylesheet" href="assets/fonts/font-awesome.min.css">
		<link rel="stylesheet" href="assets/fonts/themify-icons.css">
		<!--- owl carousel Css-->
		<link rel="stylesheet" href="assets/owlcarousel/css/owl.carousel.css">

		<!--slicknav Css-->
        <link rel="stylesheet" href="assets/css/slicknav.css">
		<!-- animate CSS -->
		<link rel="stylesheet" href="assets/css/animate.css">
		<!-- Style CSS -->
		<link rel="stylesheet" href="assets/css/style.css">
		<link rel="stylesheet" href="assets/css/responsive.css">
        <!--Popup CSS-->
		<link rel="stylesheet" href="assets/css/popup.css">

        <div class="body-particles"></div>

        <!-- particles.js container -->
        <div id="particles-js"></div>
		<!-- START PRELOADER -->
		<div class="preloader">
			<div class="status">
				<div class="status-mes"></div>
			</div>
		</div>
		<!-- END PRELOADER -->

		<!-- START NAVBAR -->
		<div id="navigation" class="fixed-top navbar-light bg-faded site-navigation">
			<div class="container">
				<div class="row">
					<div class="col-lg-3 col-md-3 col-sm-4">
						<div class="site-logo">
							<a class ="navbar-logo" href="index.html"><img src="assets/img/mytrade_logo.png"  alt=""></a>
						</div>
					</div><!--- END Col -->
					<div class="col-lg-9 col-md-9 col-sm-8">
						<div class="header_right">
							<nav id="main-menu" class="ml-auto">
								<ul>
								<li><a href="index.html">Home</a></li>
								<li><a href="index.html#about">About</a></li>
								<li><a href="index.html#forex">Forex</a></li>
								<li><a href="index.html#business">Business</a></li>
								<li><a href="index.html#faq">faq</a></li>
								<li><a href="index.html#download">Get App</a></li>
								<li><a href="index.html#contact">Contact Us</a></li>
								<li><a href="#" class ="block-menu" id="modal_trigger" hre="#modal">Login</a></li>
								</ul>
							</nav>
							<div id="mobile_menu"></div>
						</div>
					</div><!--- END Col -->
				</div><!--- END ROW -->
			</div><!--- END CONTAINER -->
		</div>
		<!-- END NAVBAR -->

        <!--START LOGIN-->
		<div id="modal" class ="popupContainer" style="position:fixed;z-index:2;opacity: 1;z-index: 11000;left: 50%;margin-left: -165px;
    top: 100px;">
			<header class ="popupHeader">
				<span class ="header_title">Login</span>
				<span class ="modal_close"><i class ="fa fa-times"></i></span>
			</header>

			<div class ="popupBody">
				<!--Social Login-->
				<div class ="social_login forget_form">
					<div class ="">
					<form  action="/Home/ForgetPasswordAction" method="Post">
						<label>Login Id</label>
						<input type="text" id="LoginId" name="LoginId" placeholder="LoginId" class ="form-control" required/>
						<br />

						<label>Mobile No.</label>
						<input type="text" id="MobileNo" name="MobileNo" placeholder="Mobile No" class ="form-control" required/>
						<br />
						<div class ="action_btns">
							<div>Already have an account ?<a href="#" class ="login_modal"> Login</a></div>
							   <button type="submit" class ="abtn" name="forgetpassword" id="forgetpassword">Forget Password</button>
						</div>
					</form>
				</div>
				</div>

				<!--Username & Password Login form-->
				<div class ="user_login">
					<form action="/Home/LoginAction" method="Post">
						<label>Login ID</label>
						<input type="text" name="LoginId" id="LoginId" placeholder="LoginID" required/>
						<br />

						<label>Password</label>
						<input type="password" name="Password" id="Password" placeholder="Password" required/>
						<br />
                        <a href="#" class ="back_btn"> Forget Password ?</a>
						<div class ="action_btns">
							<div class ="one_half last"><button type="submit" class ="abtn" name="loginabtn" id="loginabtn">Login</button></div>
						</div>
					</form><br/>
                    <!--<center>Don't have an account? <a href="#" id="register_form" class="text-center text-white">Sign up</a></center>-->

				</div>

				<!--Register Form-->
				<div class ="user_register">
					<form>
						<label>Full Name</label>
						<input type="text" />
						<br />

						<label>Email Address</label>
						<input type="email" />
						<br />

						<label>Password</label>
						<input type="password" />
						<br />

						<div class ="action_btns">
							<div>Already have an account ?<a href="#" class ="login_modal" id=""> Login</a></div>
						</div>
                       <center> <a href="#" class ="abtn">Register</a></center>
					</form>
				</div>
			</div>
		</div>
		<!--END LOGIN-->


`);