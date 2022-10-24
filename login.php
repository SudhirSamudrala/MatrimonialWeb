<?php
session_start();
include "config.php";
if (isset($_POST["login"])) {
    $email = $_POST["useremail"];
    $password = md5($_POST["password"]);
    $query = mysqli_query($con, "call login('$email','$password')");
    $num = mysqli_fetch_array($query);
    if ($num > 0) {
        $_SESSION["login"] = $_POST["useremail"];
        header("location:welcome.php");
    } else {
        $_SESSION["login"] = $_POST["useremail"];
        echo "<script>alert('Credentials are not correct');</script>";
        $extra = "login.php";
    }
}
?>
<html>
   <head>
      <meta charset="utf-8">
      <title>Login Page</title>
      <meta name="viewport" content="width=device-width, initial-scale=1">
      <link href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.0/css/bootstrap.min.css" rel="stylesheet">
      <link rel="stylesheet" href="css2.css">
      <script src="http://code.jquery.com/jquery-1.11.1.min.js"></script>
      <script src="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.0/js/bootstrap.min.js"></script>
   </head>
   <body>
      <div class="login-page">
         <div class="form">
            <form class="login-form" method="post">
               <input type="text" placeholder="Email" name="useremail" required />
               <input type="password" placeholder="Password" name="password" required />
               <button type="submit" name="login">Login</button>
               <p class="message">Not Registered? <a href="index.php">Create Account</a></p>
            </form>
         </div>
      </div>
      <script >
         $('.message a').click(function(){
            $('form').animate({height: "toggle", opacity: "toggle"}, "slow");
         });
      </script>
   </body>
</html>