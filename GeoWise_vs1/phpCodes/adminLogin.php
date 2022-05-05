<?PHP
    include "config.php"; // config.php yi çağırıyoruz

    if(isset($_POST["Email"], $_POST["Password"]))
    { 
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
        

        $sql = "SELECT * FROM admins WHERE Email='$email' AND Password='$password'";//eposta ve sifre kontrolü
		$user = mysqli_query($conn,$sql) or trigger_error(mysqli_error($conn)." ".$sql);

		if(!$user){
            echo "-1";//hata//sistem hatası vs
            exit();

        }
        elseif(mysqli_num_rows($user) == 0){//bu bilgilerde kayıtlı bilgi mevcut değil
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
                    if($row['Email'] === $email)
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