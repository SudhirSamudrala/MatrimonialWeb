<?php
require_once "config.php";
if (!empty($_POST["emailid"])) {
    $email = $_POST["emailid"];

    $result = mysqli_query($con, "call checkavailbilty('$email')");
    $count = mysqli_num_rows($result);
    if ($count > 0) {
        echo "<span style='color:red'>Please Provide Other Email Address</span>";
        echo "<script>$('#submit').prop('disabled',true);</script>";
    } else {
        echo "<span style='color:green'>You Can Use This Email Address</span>";
        echo "<script>$('#submit').prop('disabled',false);</script>";
    }
}
?>