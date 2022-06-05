<?PHP
    include "config.php"; // config.php çağırıyoruz

    if(isset($_POST["id"]))
    { // Kullanıcıdan veri gelmişmi kontrol ediyoruz.
  
        $id = ($_POST['id']);

        $userInfo="SELECT * FROM users WHERE ID='$id'";
        $result = mysqli_query($conn,$userInfo);

		if($result)
        {
            while($row=mysqli_fetch_assoc($result))
            {
                echo $row['UserName'];
            }
        }
        else{

            echo "error";
        }	
	}

?>