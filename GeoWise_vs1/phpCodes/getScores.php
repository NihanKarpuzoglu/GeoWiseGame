<?php
 include "config.php";
#$connection = mysqli_connect("localhost","root","","geowisedb");

$sql = "SELECT * FROM users ORDER BY Score DESC";
$result = mysqli_query($conn,$sql);

if($result)
{
    while($row=mysqli_fetch_assoc($result))
    {
        echo $row['UserName'].",".$row["Score"] . "*";
    }
}
else{

    echo "error";
}
