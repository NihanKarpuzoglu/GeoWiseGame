<?PHP
    include "config.php"; // config.php çağırıyoruz

    if(isset($_POST["username"], $_POST["mail"], $_POST["password"], $_POST["repeatpassword"]))
    { // Kullanıcıdan veri gelmişmi kontrol ediyoruz.

        
        
       
        function validate($data){
            $data = trim($data);
            $data = stripslashes($data);
            $data = htmlspecialchars($data);
            return $data;
         }

        $uname = validate($_POST['username']);
        $email = validate($_POST['mail']);
        $password = validate($_POST['password']);
        $password_again = validate($_POST['repeatpassword']);
        

        $uname=str_replace("'","*****",$uname); 
        $email=str_replace("'","*****",$email);
        $password=str_replace("'","*****",$password);
        $password_again=str_replace("'","*****",$password_again);

        $user_exist1="SELECT * FROM users WHERE Email='$email'";
        $user_exist2="SELECT * FROM users WHERE UserName='$uname'";
		//$user_exist_ar=mysqli_query($conn,$user_exist1);
        //$user_exist_ar2=mysqli_query($conn,$user_exist2);
        
        $user_exist_ar=mysqli_query($conn,$user_exist1)or trigger_error(mysqli_error($conn)." ".$user_exist1);
        $user_exist_ar2=mysqli_query($conn,$user_exist2)or trigger_error(mysqli_error($conn)." ".$user_exist2);
        
        
		if(mysqli_num_rows($user_exist_ar)>0)//aynı email ile hesap var
		{
			echo "-1";//aynı epostada başka bir kullanıcı mevcut
			exit();
		}
        else if(mysqli_num_rows($user_exist_ar2)>0)//username mevcut
        {
            echo "-2";//
			exit();
        }
        

        else
        {
            
            
            

			$sql = "INSERT INTO users (ID, Email, UserName, Password) VALUES (NULL, '$email', '$uname', '$password')";
			$user = mysqli_query($conn,$sql) or trigger_error(mysqli_error($conn)." ".$sql);
            
			//$user=$conn->query($sql);
            echo "1";//başarılı
			exit();
        }	

					
				
	}

      
    
    
?>