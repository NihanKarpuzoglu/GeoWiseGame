<?PHP
    include "config.php"; // config.php çağırıyoruz

    if(isset($_POST["id"], $_POST["score"]))
    { // Kullanıcıdan veri gelmişmi kontrol ediyoruz.
       
        //veriler zaten sistem üzerinden geldi
        $_id = ($_POST['id']);
        $_score = ($_POST['score']);
         
        $current_score_sql="SELECT * FROM users WHERE ID='$_id'";
        $result = mysqli_query($conn,$current_score_sql);
        if($result)
        {
            while($row=mysqli_fetch_assoc($result))
            {
                $_old_score =  $row["Score"];
            }
        }
        $_newscore = $_old_score + $_score;
        
        $sql = "UPDATE users SET Score='$_newscore' WHERE ID='$_id'";

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