<?PHP
    include "config.php"; // config.php çağırıyoruz

    if(isset($_POST["mail"]))
    { // Kullanıcıdan veri gelmişmi kontrol ediyoruz.
        function validate($data){
            $data = trim($data);
            $data = stripslashes($data);
            $data = htmlspecialchars($data);
            return $data;
         }

        $email = validate($_POST['mail']);
        $email=str_replace("'","*****",$email);

        $user_exist1="SELECT * FROM users WHERE Email='$email'";
        $user_exist_ar=mysqli_query($conn,$user_exist1)or trigger_error(mysqli_error($conn)." ".$user_exist1);

		if(mysqli_num_rows($user_exist_ar)>0)//aynı email ile hesap var
		{
			echo "1";//hesap mevcut
			exit();
		}
        else
        {
            echo "-1";//hesap mevcut değil
			exit();
        }	

					
				
	}

      
    
    
?>