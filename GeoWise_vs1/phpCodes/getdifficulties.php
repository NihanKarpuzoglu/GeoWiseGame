<?php
 include "config.php";
#$connection = mysqli_connect("localhost","root","","geowisedb");

$sql = "SELECT * FROM difficulties ORDER BY id ASC";
$result = mysqli_query($conn,$sql);

if($result)
{
    while($row=mysqli_fetch_assoc($result))
    {
        echo $row['id'].",".$row["name"] . "*";
    }
}
else{

    echo "error";
}
