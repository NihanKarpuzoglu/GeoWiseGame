<?php
	include 'config.php';
	
	$user_answer=$_POST['user_answer'];
	//echo 'hello'.$user_answer.'hello';
	
	$sql2="SELECT id FROM cities WHERE name = '$user_answer'";
	$result=mysqli_query($conn,$sql2);
	//echo mysqli_num_rows($result);
	if(mysqli_num_rows($result)==1)
	{
		$row=mysqli_fetch_assoc($result);
		
			//$city_id=(string)$row['city_id'];
		echo $row['id'];
			//$sql_city="SELECT name FROM cities WHERE id=$city_id";
			//$result_city=mysqli_query($conn,$sql_city);
			//if(mysqli_num_rows($result_city) == 1)
			//{
			//	$answer=mysqli_fetch_assoc($result_city);
			//	echo "Soru: ".$row['title']."   Cevap: ".$answer['name']."<br>";
			//}
		
	}
	else{
		echo "There is no such answer";
	}

?>