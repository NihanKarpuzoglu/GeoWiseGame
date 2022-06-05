<?PHP
    include "config.php"; 

    
//config code
/*
	$servername="localhost";
	$username="root";
	$password="";
	$dbname="geowisedb";
	$conn=new mysqli($servername, $username, $password, $dbname);
	if (!$conn) {
		die("Connection failed: " . mysqli_connect_error());
	  }

*/
if(isset($_POST["title"], $_POST["difficultly_id"], $_POST["category_id"], $_POST["city_id"]))

{
    $title = $_POST["title"];
	$difficultly_id = $_POST["difficultly_id"];
	$category_id = $_POST["category_id"];
	$city_id = $_POST["city_id"];
	
	$query2 = "INSERT INTO questions(id,title,difficultly_id,category_id,city_id) VALUES  (NULL,'$title','$difficultly_id','$category_id','$city_id')";
	
	$query2result=mysqli_query($conn,$query2);
	//$query2result=$conn->query($query2);
	if($query2result)
	{
		echo "basariyla kaydedildi";
	}
	else{
		echo "basarisiz";
	}
}

?>