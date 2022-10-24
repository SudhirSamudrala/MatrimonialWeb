<?php
include "config.php";
if (isset($_POST["register"])) {
    $fname = $_POST["fname"];
    $email = $_POST["email"];
    $password = md5($_POST["password"]);
    $query = mysqli_query(
        $con,
        "call registration('$fname','$email','$password')"
    );
    if ($query) {
        echo "<script>alert('Registration Completed');</script>";
        echo "<script>window.location.href='login.php'</script>";
    } else {
        echo "<script>alert('Registration Incomplete');</script>";
        echo "<script>window.location.href='index.php'</script>";
    }
}
?>
<html>
   <head>
      <title>Matrimonial Registration</title>
      <meta name="viewport" content="width=device-width, initial-scale=1">
      <link href="https://netdna.bootstrapcdn.com/twitter-bootstrap/2.3.2/css/bootstrap-combined.min.css" rel="stylesheet">
      <script src="https://code.jquery.com/jquery-1.11.1.min.js"></script>
      <script src="https://netdna.bootstrapcdn.com/twitter-bootstrap/2.3.2/js/bootstrap.min.js"></script>
      <script>
         function checkAvailability() {
         $("#loaderIcon").show();
         jQuery.ajax({
         url: "email_availability.php",
         data:'emailid='+$("#email").val(),
         type: "POST",
         success:function(data){
         $("#user-availability-status").html(data);
         $("#loaderIcon").hide();
         },
         error:function (){}
         });
         }
      </script>
   </head>
   <body>
      <form class="form-horizontal"  method="post">
         <link rel="stylesheet" href="css2.css">
         <fieldset>
            <div id="legend">
               <legend align="center" style="font-size: 35px;">Register</legend>
            </div>
            <div class="control-group">
               <!-- Fullname -->
               <label class="control-label"  for="fname">Enter Full Name</label>
               <div class="controls">
                  <input type="text" id="name" name="fname" placeholder="" class="input-xlarge" required>
               </div>
            </div>
            <div class="control-group">
               <!-- E-mail -->
               <label class="control-label" for="email">Enter Email</label>
               <div class="controls">
                  <input type="email" id="email" name="email" placeholder="" class="input-xlarge" onBlur="checkAvailability()"  required>
                  <span id="user-availability-status" style="font-size:12px;"></span>
               </div>
            </div>
            <div class="control-group">
               <!-- Password-->
               <label class="control-label" for="password">Enter Password</label>
               <div class="controls">
                  <input type="password" id="password" name="password" placeholder="" class="input-xlarge" required>
               </div>
            </div>
            <div class="control-group">
               <!-- Button -->
               <div class="controls">
                  <input  class="btn btn-success" id="submit" type="submit" value='register' name="register">
               </div>
            </div>
            <div class="control-group">
               <div class="controls">
                  <p class="message">Already Registered? <a href="login.php">Login</a></p>
               </div>
            </div>
         </fieldset>
      </form>
   </body>
</html>