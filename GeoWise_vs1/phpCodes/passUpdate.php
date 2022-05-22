<?PHP
    include "config.php"; // config.php çağırıyoruz

    if(isset($_POST["mail"], $_POST["password"]))
    { // Kullanıcıdan veri gelmişmi kontrol ediyoruz.
       
        //veriler zaten sistem üzerinden geldi
        $_email = ($_POST['mail']);
        $_password = ($_POST['password']);
                
        $sql = "UPDATE users SET Password='$_password' WHERE Email='$_email'";

        if ($conn->query($sql) === TRUE) 
        {
            echo "1";
        } 
        else 
        {
            echo "-1";
        }  
					
				
	}

      
    
    
?>