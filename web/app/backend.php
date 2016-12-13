<?php	
	namespace backend; 

	class main{
		
		function reg(){
			$template=new \Template;
			echo $template->render('backend/entrance.php');
		}

		//ADDING ITEMS
		

		function addMaterial($f3){
			if( (isset($_POST["name"])) && (isset($_POST["id_ue"])) ){
			$table=new \DB\SQL\Mapper(\F3::get('DB'),'material');						
			$table->name=\F3::get("POST.name");
			$table->id_ue=\F3::get("POST.id_ue");						
			$table->save();	
			$last_id = \F3::get('DB')->exec("SELECT id FROM material ORDER BY id DESC LIMIT 1")[0]["id"];		
			echo($last_id);
			}
			else {
				echo("error");
			}
		}
		
		function addTrademark($f3){			
			if (isset($_POST["name"])){
			$table=new \DB\SQL\Mapper(\F3::get('DB'),'trademark');						
			$table->name=\F3::get("POST.name");					
			$table->save();	
			$last_id = \F3::get('DB')->exec("SELECT id FROM trademark ORDER BY id DESC LIMIT 1")[0]["id"];		
			echo($last_id);
			}
			else {
				echo("error");
			}
		}
		
		function addAgent($f3){			
			if (isset($_POST["name"])){
			$table=new \DB\SQL\Mapper(\F3::get('DB'),'agent');						
			$table->name=\F3::get("POST.name");					
			$table->save();	
			$last_id = \F3::get('DB')->exec("SELECT id FROM agent ORDER BY id DESC LIMIT 1")[0]["id"];		
			echo($last_id);
			}
			else {
				echo("error");
			}
		}
		
		function addToSklad($f3){			
			if (isset($_POST["id_material"])){
			$table=new \DB\SQL\Mapper(\F3::get('DB'),'sklad');						
			$table->id_material=\F3::get("POST.id_material");
			$table->id_ue=\F3::get("POST.id_ue");	
			$table->value=\F3::get("POST.value");				
			$table->save();	
			$last_id = \F3::get('DB')->exec("SELECT id FROM agent ORDER BY id DESC LIMIT 1")[0]["id"];		
			echo($last_id);
			}
			else {
				echo("error");
			}
		}
		
		function addUe($f3){			
			if(isset($_POST["name"])){
				$table=new \DB\SQL\Mapper(\F3::get('DB'),'ue');									
				$table->name=\F3::get("POST.name");					
				$table->save();	
				$last_id = \F3::get('DB')->exec("SELECT id FROM ue ORDER BY id DESC LIMIT 1")[0]["id"];
				echo($last_id);
			}
			else {
				echo("error");
			}
		}

		function addZakaz($f3){			
			$result = $f3->get("DB")->exec("");
			echo($result);
		}		

	}
