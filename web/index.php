<?php
	require('lib/base.php');
	F3::set('AUTOLOAD','app/; ui/');
	require('app/backend.php');

	F3::config('app/config.ini');

	F3::set('DEBUG',3);
	F3::set('UI','ui/');
	F3::set('DB',
		new \DB\SQL(
			'mysql:host=localhost;port=3306;dbname=proga',
			'rieltor',
			'VPZ5sAGw'
		)
	);
	

	F3::run();
?>


