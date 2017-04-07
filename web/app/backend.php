<?php	
	namespace backend; 

	class edit{

		//ADDING ITEMS

		function add_material($f3){
			if((isset($_POST["name"]))){
			$table=new \DB\SQL\Mapper($f3->get('DB'),'material');						
			$table->name=$f3->get("POST.name");
			$table->id_ue=$f3->get("POST.id_ue");	
			$table->id_type=$f3->get("POST.id_type");
			$table->unit_price=$f3->get("POST.unit_price");				
			$table->save();	
			$last_id = $f3->get('DB')->exec("SELECT id FROM material ORDER BY id DESC LIMIT 1")[0]["id"];		
			echo($last_id);
			}
			else {
				echo("error");
			}
		}

		function add_zakaz_material($f3){
			if((isset($_POST["quantity"]))){
			$table=new \DB\SQL\Mapper($f3->get('DB'),'zakazmaterial');						
			$table->quantity=$f3->get("POST.quantity");
			$table->id_material=$f3->get("POST.id_material");	
			$table->id_zakaz=$f3->get("POST.id_zakaz");			
			$table->save();	
			$last_id = $f3->get('DB')->exec("SELECT id FROM zakazmaterial ORDER BY id DESC LIMIT 1")[0]["id"];		
			echo($last_id);
			}
			else {
				echo("error");
			}
		}
		
		
		function add_trademark($f3){			
			if (isset($_POST["name"])){
			$table=new \DB\SQL\Mapper($f3->get('DB'),'trademark');						
			$table->name=$f3->get("POST.name");					
			$table->save();	
			$last_id = $f3->get('DB')->exec("SELECT id FROM trademark ORDER BY id DESC LIMIT 1")[0]["id"];		
			echo($last_id);
			}
			else {
				echo("error");
			}
		}
		
		function add_agent($f3){			
			if (isset($_POST["name"])){
			$table=new \DB\SQL\Mapper($f3->get('DB'),'agent');						
			$table->name=$f3->get("POST.name");					
			$table->save();	
			$last_id = $f3->get('DB')->exec("SELECT id FROM agent ORDER BY id DESC LIMIT 1")[0]["id"];		
			echo($last_id);
			}
			else {
				echo("error");
			}
		}
		
		function add_to_sklad($f3){			
			if (isset($_POST["id_material"])){
			$table=new \DB\SQL\Mapper($f3->get('DB'),'sklad');						
			$table->id_material=$f3->get("POST.id_material");
			$table->quantity=$f3->get("POST.quantity");			
			$table->save();	
			$last_id = $f3->get('DB')->exec("SELECT id FROM sklad ORDER BY id DESC LIMIT 1")[0]["id"];		
			echo($last_id);
			}
			else {
				echo("error");
			}
		}

		function add_to_ostatok($f3){			
			if (isset($_POST["id_material"])){
			$table=new \DB\SQL\Mapper($f3->get('DB'),'ostatok');						
			$table->id_material=$f3->get("POST.id_material");
			$table->quantity=$f3->get("POST.quantity");			
			$table->save();	
			$last_id = $f3->get('DB')->exec("SELECT id FROM ostatok ORDER BY id DESC LIMIT 1")[0]["id"];		
			echo($last_id);
			}
			else {
				echo("error");
			}
		}
		
		function add_ue($f3){			
			if(isset($_POST["name"])){
				$table=new \DB\SQL\Mapper($f3->get('DB'),'ue');									
				$table->name=$f3->get("POST.name");					
				$table->save();	
				$last_id = $f3->get('DB')->exec("SELECT id FROM ue ORDER BY id DESC LIMIT 1")[0]["id"];
				echo($last_id);
			}
			else {
				echo("error");
			}
		}

		function add_type($f3){			
			if(isset($_POST["name"])){
				$table=new \DB\SQL\Mapper($f3->get('DB'),'type');									
				$table->name=$f3->get("POST.name");					
				$table->save();	
				$last_id = $f3->get('DB')->exec("SELECT id FROM type ORDER BY id DESC LIMIT 1")[0]["id"];
				echo($last_id);
			}
			else {
				echo("error");
			}
		}

		

		static function add_zakaz($f3){			
			if(isset($_POST["id_trademark"])){
				$table=new \DB\SQL\Mapper($f3->get('DB'),'zakaz');									
				$table->id_trademark=$f3->get("POST.id_trademark");	
				$table->id_agent=$f3->get("POST.id_agent");	
				$table->quantity=$f3->get("POST.quantity");	
                $table->num_zakaz=$f3->get("POST.num_zakaz");					
				$table->save();	
				$last_id = $f3->get('DB')->exec("SELECT id FROM zakaz ORDER BY id DESC LIMIT 1")[0]["id"];
				echo($last_id);
			}
			else {
				echo("error");
			}
		}

        function edit_type($f3){			
			if(isset($_POST["id"])){
				$table=new \DB\SQL\Mapper($f3->get('DB'),'type');
				$table->load(array('id=?', $_POST['id']));									
				$table->name=$f3->get("POST.name");					
				$table->update();	
				echo("success");
			}
			else {
				echo("error");
			}
		}

		function edit_ue($f3){			
			if(isset($_POST["id"])){
				$table=new \DB\SQL\Mapper($f3->get('DB'),'ue');
				$table->load(array('id=?', $_POST['id']));									
				$table->name=$f3->get("POST.name");					
				$table->update();	
				echo("success");
			}
			else {
				echo("error");
			}
		}

		function edit_agent($f3){			
			if(isset($_POST["id"])){
				$table=new \DB\SQL\Mapper($f3->get('DB'),'agent');
				$table->load(array('id=?', $_POST['id']));									
				$table->name=$f3->get("POST.name");					
				$table->update();	
				echo("success");
			}
			else {
				echo("error");
			}
		}

		function edit_trademark($f3){			
			if(isset($_POST["id"])){
				$table=new \DB\SQL\Mapper($f3->get('DB'),'trademark');
				$table->load(array('id=?', $_POST['id']));									
				$table->name=$f3->get("POST.name");					
				$table->update();	
				echo("success");
			}
			else {
				echo("error");
			}
		}
		
		function remove_from_sklad($f3){			
			if (isset($_POST["id"])){
			$table=new \DB\SQL\Mapper($f3->get('DB'),'sklad');						
			$table->id_material=$f3->get("POST.id_material");
			$table->id_ue=$f3->get("POST.id_ue");	
			$table->quantity=$f3->get("POST.quantity");
			$table->type=0;					
			$table->save();	
			$last_id = $f3->get('DB')->exec("SELECT id FROM sklad ORDER BY id DESC LIMIT 1")[0]["id"];		
			echo($last_id);
			}
			else {
				echo("error");
			}
		}
		
		//EDITING ITEMS
		
		function edit_zakaz($f3){			
			if(isset($_POST["id_trademark"])){
				$table=new \DB\SQL\Mapper($f3->get('DB'),'zakaz');	
				$table->load(array("id = ?", $_POST["id"]));								
				$table->id_trademark=$f3->get("POST.id_trademark");	
				$table->id_agent=$f3->get("POST.id_agent");	
				$table->quantity=$f3->get("POST.quantity");					
				$table->update();	
				$last_id = $f3->get('DB')->exec("SELECT id FROM zakaz ORDER BY id DESC LIMIT 1")[0]["id"];
				$f3->get('DB')->exec("DELETE FROM zakaz_material WHERE id_zakaz = ?", $_POST["id"]);
				foreach($_POST["material"] as $material){
					$f3->get('DB')->exec("INSERT INTO zakaz_material (id_zakaz, id_material, quantity) VALUES ($last_id, $material[id], $material[quantity])");
				}
				echo($last_id);
			}
			else {
				echo("error");
			}
		}
		
		//REMOVING ITEMS
		
		function remove_material($f3){
			if(isset($_POST["id"])){
				$table=new \DB\SQL\Mapper($f3->get('DB'),'material');
				$table->load(array('id=?',$_POST['id']));
				$table->erase();
				echo("success");
			}			
			else {
				echo("error");
				}
		}
		function remove_agent($f3){
			if(isset($_POST["id"])){
				$table=new \DB\SQL\Mapper($f3->get('DB'),'agent');
				$table->load(array('id=?',$_POST['id']));
				$table->erase();
				echo("success");
			}			
			else {
				echo("error");
				}
		}
		function remove_type($f3){
			if(isset($_POST["id"])){
				$table=new \DB\SQL\Mapper($f3->get('DB'),'type');
				$table->load(array('id=?',$_POST['id']));
				$table->erase();
				echo("success");
			}			
			else {
				echo("error");
				}
		}
		function remove_ue($f3){
			if(isset($_POST["id"])){
				$table=new \DB\SQL\Mapper($f3->get('DB'),'ue');
				$table->load(array('id=?',$_POST['id']));
				$table->erase();
				echo("success");
			}			
			else {
				echo("error");
				}
		}
		
		function remove_trademark($f3){
			if(isset($_POST["id"])){
				$table=new \DB\SQL\Mapper($f3->get('DB'),'trademark');
				$table->load(array('id=?',$_POST['id']));
				$table->erase();
				echo("success");
			}			
			else {
				echo("error");
				}
		}
		function remove_zakaz($f3){
			if(isset($_POST["id"])){
				$table=new \DB\SQL\Mapper($f3->get('DB'),'zakaz');
				$table->load(array('id=?',$_POST['id']));
				$table->erase();
				echo("success");
			}			
			else {
				echo("error");
				}
		}

		function remove_sklad($f3){
			if(isset($_POST["id"])){
				$table=new \DB\SQL\Mapper($f3->get('DB'),'sklad');
				$table->load(array('id=?',$_POST['id']));
				$table->erase();
				echo("success");
			}			
			else {
				echo("error");
				}
		}	

		function remove_from_ostatok($f3){
			if(isset($_POST["id"])){
				$table=new \DB\SQL\Mapper($f3->get('DB'),'ostatok');
				$table->load(array('id=?',$_POST['id']));
				$table->erase();
				echo("success");
			}			
			else {
				echo("error");
				}
		}				

	}
	class get{
		function get_agent($f3){
			$result = $f3->get("DB")->exec("SELECT * FROM agent");
			die(json_encode($result, JSON_UNESCAPED_UNICODE));
		}
		function get_ue($f3){
			$result = $f3->get("DB")->exec("SELECT * FROM ue");
			die(json_encode($result, JSON_UNESCAPED_UNICODE));
		}
		function get_type($f3){
			$result = $f3->get("DB")->exec("SELECT * FROM type");
			die(json_encode($result, JSON_UNESCAPED_UNICODE));
		}
		
		function get_trademark($f3){
			$result = $f3->get("DB")->exec("SELECT * FROM trademark");
			die(json_encode($result, JSON_UNESCAPED_UNICODE));
		}
		
		function get_material($f3){
			$result = $f3->get("DB")->exec("SELECT material.id as id, material.name as name, ue.id as id_ue, type.id as id_type, material.unit_price as unit_price FROM material INNER JOIN ue ON ue.id = material.id_ue INNER JOIN type ON type.id = material.id_type");
			die(json_encode($result, JSON_UNESCAPED_UNICODE));
		}
        function get_zakaz_material($f3){
			$result = $f3->get("DB")->exec("SELECT zakazmaterial.id as id, zakazmaterial.quantity as quantity, material.id as id_material, zakaz.num_zakaz as id_zakaz FROM zakazmaterial INNER JOIN zakaz ON zakaz.num_zakaz = zakazmaterial.id_zakaz INNER JOIN material ON material.id = zakazmaterial.id_material");
			die(json_encode($result, JSON_UNESCAPED_UNICODE));
		}
		function get_zakaz($f3){
			$result = $f3->get("DB")->exec("SELECT zakaz.id as id, sum(zakazmaterial.quantity*material.unit_price) as price, zakaz.num_zakaz as num_zakaz, zakaz.quantity as quantity, zakaz.datetime as datetime, agent.id as id_agent, trademark.id as id_trademark FROM zakaz INNER JOIN agent ON agent.id = zakaz.id_agent INNER JOIN trademark ON trademark.id = zakaz.id_trademark left join zakazmaterial ON zakazmaterial.id_zakaz = zakaz.num_zakaz LEFT JOIN material ON material.id = zakazmaterial.id_material GROUP BY zakaz.id");
			die(json_encode($result, JSON_UNESCAPED_UNICODE));
		}
		
		function get_sklad($f3){
			$result = $f3->get("DB")->exec("SELECT sklad.id, material.id as id_material, sklad.quantity as quantity, sklad.datetime as datetime FROM sklad
											INNER JOIN material ON material.id = sklad.id_material");
			die(json_encode($result, JSON_UNESCAPED_UNICODE));
		}

		function get_ostatok($f3){
			$result = $f3->get("DB")->exec("SELECT res.id_material as id_material, min(res.quantity) as quantity FROM
(SELECT t1.id_material as id_material, (t2.quantity - t1.quantity) as quantity 
from
((SELECT sklad.id_material as id_material, sum(zakazmaterial.quantity) as quantity
FROM sklad join zakazmaterial on zakazmaterial.id_material = sklad.id_material) as t1) INNER join
((SELECT sklad.id_material as id_material, sum(sklad.quantity) as quantity
FROM sklad) as t2) on t1.id_material = t2.id_material
union
SELECT sklad.id_material as id_material, sum(sklad.quantity) as quantity from sklad GROUP by id) as res
GROUP by id_material");
			die(json_encode($result, JSON_UNESCAPED_UNICODE));
		}
	}

	