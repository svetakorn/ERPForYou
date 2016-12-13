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
			$table->quantity=\F3::get("POST.quantity");
			$table->type=1;				
			$table->save();	
			$last_id = \F3::get('DB')->exec("SELECT id FROM sklad ORDER BY id DESC LIMIT 1")[0]["id"];		
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
			if(isset($_POST["id_trademark"])){
				$table=new \DB\SQL\Mapper(\F3::get('DB'),'zakaz');									
				$table->id_trademark=\F3::get("POST.id_trademark");	
				$table->id_agent=\F3::get("POST.id_agent");	
				$table->number=\F3::get("POST.number");					
				$table->save();	
				$last_id = \F3::get('DB')->exec("SELECT id FROM zakaz ORDER BY id DESC LIMIT 1")[0]["id"];
				foreach($_POST["material"] as $material){
					\F3::get('DB')->exec("INSERT INTO zakaz_material (id_zakaz, id_material, quantity) VALUES ($last_id, $material['id'], $material['quantity'])");
				}
				echo($last_id);
			}
			else {
				echo("error");
			}
		}
		
		function removeFromSklad($f3){			
			if (isset($_POST["id"])){
			$table=new \DB\SQL\Mapper(\F3::get('DB'),'sklad');						
			$table->id_material=\F3::get("POST.id_material");
			$table->id_ue=\F3::get("POST.id_ue");	
			$table->quantity=\F3::get("POST.quantity");
			$table->type=0;					
			$table->save();	
			$last_id = \F3::get('DB')->exec("SELECT id FROM sklad ORDER BY id DESC LIMIT 1")[0]["id"];		
			echo($last_id);
			}
			else {
				echo("error");
			}
		}
		//EDITING ITEMS
		
		function editZakaz($f3){			
			if(isset($_POST["id_trademark"])){
				$table=new \DB\SQL\Mapper(\F3::get('DB'),'zakaz');	
				$table->load(array("id = ?", $_POST["id"]));								
				$table->id_trademark=\F3::get("POST.id_trademark");	
				$table->id_agent=\F3::get("POST.id_agent");	
				$table->number=\F3::get("POST.number");					
				$table->update();	
				$last_id = \F3::get('DB')->exec("SELECT id FROM zakaz ORDER BY id DESC LIMIT 1")[0]["id"];
				\F3::get('DB')->exec("DELETE FROM zakaz_material WHERE id_zakaz = ?", $_POST["id"]);
				foreach($_POST["material"] as $material){
					\F3::get('DB')->exec("INSERT INTO zakaz_material (id_zakaz, id_material, quantity) VALUES ($last_id, $material['id'], $material['quantity'])");
				}
				echo($last_id);
			}
			else {
				echo("error");
			}
		}
		
		//REMOVING ITEMS
		
				

	}
