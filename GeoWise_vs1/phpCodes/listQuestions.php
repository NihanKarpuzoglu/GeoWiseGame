<?php
 include "config.php";
#$connection = mysqli_connect("localhost","root","","geowisedb");

$sql = "SELECT * FROM questions ORDER BY id DESC";
$result = mysqli_query($conn,$sql);

if($result)
{
    while($row=mysqli_fetch_assoc($result))
    {
        echo $row["title"] . ",". $row["difficultly_id"] . ",".$row["category_id"].",".$row["city_id"]."*";
    }
}
else{

    echo "error";
}
