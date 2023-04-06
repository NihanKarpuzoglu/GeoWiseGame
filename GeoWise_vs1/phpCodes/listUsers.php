<?php
 include "config.php";
#$connection = mysqli_connect("localhost","root","","geowisedb");

$sql = "SELECT * FROM users ORDER BY ID DESC";

//s$query = "select person_id,score,date from table1, table2 where table1.{$rollno}=table2.{$rollno}";

$result = mysqli_query($conn,$sql);

if($result)
{
    while($row=mysqli_fetch_assoc($result))
    {
        echo $row["Email"] . ",". $row["UserName"] . "*";
    }
}
else{

    echo "error";
}
