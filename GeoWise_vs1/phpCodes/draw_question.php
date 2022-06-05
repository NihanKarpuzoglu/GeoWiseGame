
<?php
		include 'config.php';
		
		//WHERE id==1
		$difficulty_id=$_POST['difficulty_id'];
		$category_id=$_POST['category_id'];
		
		$sql="SELECT * FROM questions WHERE difficultly_id = '$difficulty_id' AND category_id = '$category_id'";
		$result=mysqli_query($conn,$sql);
		
		if(mysqli_num_rows($result)>0)
		{
			while($row=mysqli_fetch_assoc($result))
			{
				//$city_id=(string)$row['city_id'];
				echo $row['title'].','.$row['city_id'].'*';
				//$sql_city="SELECT name FROM cities WHERE id=$city_id";
				//$result_city=mysqli_query($conn,$sql_city);
				//if(mysqli_num_rows($result_city) == 1)
				//{
				//	$answer=mysqli_fetch_assoc($result_city);
				//	echo "Soru: ".$row['title']."   Cevap: ".$answer['name']."<br>";
				//}
			}
		}
		else{
			echo "There is no such question for chosen category and difficultly level";
		}

?>