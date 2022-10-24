<?php
session_start();
include('config.php');
//validating session
if(strlen($_SESSION['login'])==0)
  {
header('location:login.php');
}
else{
?>
<html>
   <head>
      <meta>
      <title>Matrimonial Project</title>
      <meta name="viewport" content="width=device-width, initial-scale=1">
      <link href="http://netdna.bootstrapcdn.com/bootstrap/3.0.0/css/bootstrap.min.css" rel="stylesheet">
      <link rel="stylesheet" href="css.css">
      <script src="http://code.jquery.com/jquery-1.11.1.min.js"></script>
      <script src="http://netdna.bootstrapcdn.com/bootstrap/3.0.0/js/bootstrap.min.js"></script>
   </head>
   <body>
      <div class="container">
         <div class="row">
            <div class="col-md-12">
               <div class="error-template">
                  <?php
                     $userid=$_SESSION['login'];
                      $query=mysqli_query($con,"select FullName from userstable where EmailId='$userid'");
                     while($row=mysqli_fetch_array($query))
                     {?>
                  <h1>Welcome : <?php echo htmlentities($row['FullName']);?></h1>
                  <?php } ?>
                  <div class="error-actions">
                     <a href="logout.php" class="btn btn-primary btn-lg"><span class="glyphicon glyphicon-log-out"></span>
                     Logout  </a>
                  </div>
               </div>
            </div>
         </div>
      </div>
   </body>
</html>
<?php } ?>