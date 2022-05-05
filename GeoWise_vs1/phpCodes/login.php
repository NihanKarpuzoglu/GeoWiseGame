<?PHP
    include "config.php"; // config.php yi çağırıyoruz

    if(isset($_POST["Email"], $_POST["Password"]))
    { // Kullanıcı adı ve şifre verileri gelmişmi kontrol ediyoruz.

        function validate($data){
            $data = trim($data);
            $data = stripslashes($data);
            $data = htmlspecialchars($data);
            return $data;
        }

        $email = validate($_POST["Email"]); // Gelen kullanıcı adı verisini kullaniciAdi değişkenine atıyoruz.
        $password = validate($_POST["Password"]); // Gelen şifre verisini sifre değişkenine atıyoruz

        $email=str_replace("'","*****",$email); 
        $password=str_replace("'","*****",$password);
        

        $sql = "SELECT * FROM users WHERE (UserName='$email' OR Email='$email') AND password='$password'";//kullanıcı ismi veya epostasıyla giriş yapabilir
		$user = mysqli_query($conn,$sql) or trigger_error(mysqli_error($conn)." ".$sql);

		if(!$user){
            echo "-1";//hata//sistem hatası vs
            exit();

        }
        elseif(mysqli_num_rows($user) == 0){//bu bilgilerde kayıtlı kullanıcı mevcut değil
            echo "-2"; 
            exit();

        }
        else
        {
            if (mysqli_num_rows($user) === 1) 
            {//dönen satır mevcut
                $row = mysqli_fetch_assoc($user);
                if ($row['Password'] === $password)
                {//içerde tekrar kontrol gerçekleştir//parola eşleşiyor mu
                    if($row['UserName'] === $email)
                    {//kullanıcı ismiyle giriş
                        echo "1";//giriş başarılı	
                        exit();
                    }
                    else if($row['Email'] === $email)
                    {//mail adresiyle giriş
                        echo "1";//giriş başarılı	
                        exit();
                    }
                }
                else
                {//
                    echo "-2";//bilgilerin eşleşmesinde hata oluştu
                    exit();
                }
            }
        }
            
    }
    

?>