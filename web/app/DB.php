<?php
/******************************************

    PHP FRAMEWORK FOR EASIEST USING MYSQL
    
    Author: Trushin Victor
    09/2013

******************************************/


class DB{
    
    /* ------ class properties ------ */
    
    # configuration
    private $config = array(
        'host' => 'localhost',
        'username' => 'admin',
        'password' => 'admin',
        'charset' => 'utf8',
        'collation' => 'utf8_bin',
        'add_slashes' => false        
    );
    
    # status of connection
    public $status = false;
    
    # connection link
    private $connection;
    
    # current table
    private $table = "";
    
    # limit start index
    private $limitStart = 0;
    
    # limit count
    private $limitCount = 0;
    
    # select columns names
    private $cols = "";
    
    # error status
    public $is_error = false;
    
    # error container
    public $error = "";
    
    /* ------ class methods ------ */    
        
    /*
    * CHANGE CONFIGURATION
    * 
    * param: newConfig array
    */
    function config($newConfig)
    {
        // execution condition checking
        if ( is_array($newConfig) )
        {
            foreach($newConfig as $key => $value)
            {
                switch ($key)
                {
                    //changing configuration to user's
                    case 'host': $this->config['host'] = $value; break;
                    case 'username' : $this->config['username'] = $value; break;
                    case 'password' : $this->config['password'] = $value; break;
                    case 'charset' : $this->config['charset'] = $value; break;
                    case 'collation' : $this->config['collation'] = $value; break;
                    case 'add_slashes' : $this->config['add_slashes'] = $value; break;
                }
            }
        }
        // there and below it means link to DB class
        return $this;
    }
    
    /*
    *   CONNECTS TO MYSQL SERVER
    */
    function connect()
    {
        // getting configuration
        $host = $this->config['host'];
        $username = $this->config['username'];
        $password = $this->config['password'];
        $charset = $this->config['charset'];
        $collation = $this->config['collation'];
        
        // connecting to MYSQL server
        if(!($this->connection = @mysql_connect($host, $username, $password)))
        {
            // there and below this code is writing error's text
            $this->is_error = true;
            $this->error = "MYSQL CONNECTION FAILED | ".mysql_error();
        } else {
            $this->status = true;
        }
        
        // setting charset and collation
        @mysql_query("set_client='{$charset}'");
        @mysql_query("set character_set_results='{$charset}'");
        @mysql_query("set collation_connection='{$collation}'");
        @mysql_query("SET NAMES {$charset}");
        
        return $this;
    }
    
    /*
    *   CLOSE CONNECTION WITH MYSQL
    */
    function close()
    {
        // execution condition checking
        if ( ($this->status) && (!($this->is_error))) {
            if(!(@mysql_close($this->connection)))
            {
                $this->is_error = true;
                $this->error = "MYSQL CLOSING ERROR";
            } else {
                $this->status = false;
            }
        } 
        return $this;
    }
    
    /*
    *   CREATE DB
    *
    *   param: name string
    */
    function newDB($name)
    {
        // execution condition checking
        if( (is_string($name)) && ($this->connection) && (!($this->is_error)) )
        {
            // forming mysql query
            $query = "CREATE DATABASE IF NOT EXISTS {$name}";
            $query.=" CHARACTER SET {$this->config['charset']} COLLATE {$this->config['collation']}";
            if (!( @mysql_query($query) ))
            {
                $this->is_error = true;
                $this->error = "DATABASE CREATING ERROR | ".mysql_error();
            }
        }
        return $this;
    }
        
    /*
    *   CREATE TABLE IN DB
    *
    *   param: tableName string
    *          params assoc array
    */
    function newTable($tableName, $params)
    {
        // execution condition checking
        if( (is_string($tableName)) && (is_array($params)) && ($this->status) && (!($this->is_error)) ){
            
            // variable data type declaration
            $name = array(); /* cantains col's name */
            $type = array(); /* contains data type. example: INT */
            
            // getting data from $params
            foreach($params as $key => $value)
            {
                $name[] = $key;
                $type[] = $value;
            }
            
            // forming part of the query
            $rows = "";
            for($i=0; $i<count($name)-1; $i++)
            {
                $rows.=$name[$i]." ".$type[$i].", ";
            }
            $rows.=$name[count($name)-1]." ".$type[count($type)-1];
            
            // forming mysql query
            $query="CREATE TABLE IF NOT EXISTS " . $tableName . " (" . $rows . ")";
            $query.=" CHARACTER SET {$this->config['charset']} COLLATE {$this->config['collation']}";
            
            if (!( @mysql_query($query) ))
            {
                $this->is_error = true;
                $this->error = "CREATING TABLE ERROR | ".mysql_error();
            }
        }
        return $this;
    }
    
    /*
    *   DROP TABLE
    *
    *   param: tableName string
    */
    function dropTable($tableName)
    {
        // execution condition checking
        if ( (is_string($tableName)) && ($this->status) && (!($this->is_error)) )
        {
            // forming mysql query
            $query="DROP TABLE IF EXISTS ".$tableName;
            
            if (!( @mysql_query($query) ))
            {
                $this->is_error = true;
                $this->error = "DROP TABLE ERROR | ".mysql_error();
            }
        }   
        return $this; 
    }
    
    /*
    *   DROP DATABASE
    *
    *   param: dbName string
    */
    function dropDB($dbName)
    {
        // execution condition checking
        if ( (is_string($dbName)) && ($this->status) && (!($this->is_error)) )
        {
            // forming mysql query
            $query="DROP DATABASE IF EXISTS ".$dbName;
            
            if (!(@mysql_query($query)))
            {
                $this->is_error = true;
                $this->error = "DROP DATABASE ERROR | ".mysql_error();
            }
        }   
        return $this; 
    }
    
    /*
    *   SELECT DATABASE
    *
    *   param: name string
    */
    function database($name)
    {
        // execution condition checking
        if( (is_string($name)) && ($this->connection) && (!($this->is_error)) )
        {
            if (!( @mysql_select_db($name, $this->connection) ))
            {
                $this->is_error = true;
                $this->error = "DATABASE SELECT ERROR | ".mysql_error();
            }
        }
        return $this;
    }
    
    /*
    *   SELECT TABLE
    *
    *   param: tableName string
    */
    function table($tableName)
    {
        // execution condition checking
        if( (is_string($tableName)) && ($this->status) && (!($this->is_error)) )
        {
            // recording table name
            $this->table = $tableName;
        }
        return $this;
    }
    
    /*
    *   LIMITS COUNT OF RETURNED ROWS
    *
    *   param: count int
    *          start int
    */
    function limit($count, $start = 0)
    {
        // execution condition checking
        if( (is_int($count)) && (is_int($start)) && ($this->status) && (!($this->is_error)) )
        {
            // recording limits
            // they will be removed after next query
            $this->limitCount = $count;
            $this->limitStart = $start;
        }
        return $this;
    }
    
    /*
    *   THIS FUNCTION SETS COLUMN NAMES, WHICH MUST BE RETURNED
    *   Entered column names will be deleted after first query
    *
    *   param: colNames string/array
    */
    function columns($colNames)
    {
        // execution condition checking
        if( (isset($colNames)) && ($this->status) && (!($this->is_error)) )
        {
            // if there is only one column needed
            if( is_string($colNames) )
            {
                $this->cols = $colNames;
            }
            // if user needs several columns
            if( is_array($colNames) )
            {
                $bufer="";
                for($i=0; $i<count($colNames)-1; $i++) $bufer.=$colNames[$i] . ", ";
                $bufer.=$colNames[count($colNames)-1];
                $this->cols = $bufer;
            }
        }
        return $this;
    }
    
    /*
    *   THIS FUNCTION RETURNS COUNT OF ROWS IN SELECTED TABLE
    */
    function count()
    {
        // execution condition checking
        if( (!($this->table=="")) && ($this->status) && (!($this->is_error)) )
        {
            // forming mysql query
            $query = "SELECT count(*) FROM " . $this->table;
            
            if($resp = @mysql_query($query))
            {
                // returning the number of records
                $row = @mysql_fetch_row($resp);
                $total = $row[0];
                return $total;
            } else {
                $this->is_error = true;
                $this->error = "COUNT CALCULATING ERROR | ".mysql_error();
                return false;
            }
        }
        
        //if somwhere before was an error it returns false
        if ($this->is_error)
        {
            return false;
        }
    }
        
    /*
    *   SEND QUERY TO MYSQL SERVER
    *
    *   param: text string
    */
    function query($text)
    {
        // execution condition checking
        if( (is_string($text)) && ($this->connection) && (!($this->is_error)) )
        {
            // sending user's query to server
            if (!($result = @mysql_query($text)))
            {
                $this->is_error = true;
                $this->error = "SQL QUERY ERROR | ".mysql_error();
            } else {
                return $result;
            }
        }
    }
    
    /*
    *   INSERT ROW INTO SELECTED TABLE
    *
    *   param: data assoc array
    */
    function insert($data)
    {
        // execution condition checking
        if( (is_array($data)) && (!($this->table=="")) && ($this->status) && (!($this->is_error)) )
        {
            // variable data type declaration
            $fields = array(); /* there are col names */
            $values = array(); /* there are inserting values */
            
            // getting data from $data
            foreach($data as $key => $value)
            {
                $fields[] = $key;
                
                // adding slashes if needed (default is needed)
                if($this->config["add_slashes"])
                {
                    $values[] = "'" . addslashes($value) . "'";
                } else {
                    $values[] = "'" . $value . "'";
                }
            }
            
            // forming part of the query
            $fieldsStr = '';
            $valuesStr = '';
            
            for($i=0; $i<count($fields)-1; $i++) $fieldsStr.=$fields[$i].", ";
            $fieldsStr.=$fields[count($fields)-1];
            
            for($i=0; $i<count($values)-1; $i++) $valuesStr.=$values[$i].", ";
            $valuesStr.=$values[count($values)-1];
            
            // forming mysql query
            $query="INSERT INTO ".$this->table." (".$fieldsStr.") VALUES (".$valuesStr.")";
            
            if (!(@mysql_query($query)))
            {
                $this->is_error = true;
                $this->error = "INSERT DATA ERROR | ".mysql_error();
            }
        }
        return $this;
    }
    
    /*
    *   UPDATES DATA IN DATABASE
    *
    *   param: find integer / array
    *          data assoc array
    */
    function update($find, $data)
    {
        // execution condition checking
        if ( (isset($find)) && (is_array($data)) && (!(count($data) == 0)) && (!($this->table == "")) && ($this->status) && (!($this->is_error)) )
        {
            $query="UPDATE " . $this->table . " SET ";
            
            // getting data from $data
            foreach($data as $key => $value)
            {
                $var[] = $key;
                // adding slashes if needed (default is needed)
                if($this->config["add_slashes"])
                {
                    $val[] = "'" . addslashes($value) . "'";
                } else {
                    $val[] = "'" . $value . "'";
                }
            }
            
            // forming part of the query
            for($i=0; $i<count($var)-1; $i++) $query.=$var[$i] . " = " . $val[$i] . ", ";
            $query.=$var[count($var)-1] . " = " . $val[count($val)-1] . " WHERE ";
            
            // if user selects record by id
            if( is_int($find) ){
                $arr = array("id", "ID", "Id", "iD");
                $ind = 0;
                $bufer = $query . $arr[$ind] . " = " . $find;
                while(!(@mysql_query($bufer)))
                {
                    if ($ind == 3)
                    {
                        $this->is_error = true;
                        $this->error = "UPDATE ERROR";
                    }
                    $ind++;
                    $bufer = $query . $arr[$ind] . " = " . $find;
                }   
            }
            
            // if user selects record with some settings
            if( is_array($find) )
            {
                // forming part of the query
                for($i=0; $i<count($find); $i++)
                {
                    if( (is_array($find[$i])) && (count($find[$i]) == 3) )
                    {
                        if(!($i == count($find)-1))
                        {
                            $query.= $find[$i][0] . " " . $find[$i][1] . " '" . $find[$i][2] . "' AND ";
                        } else {
                            $query.= $find[$i][0] . " " . $find[$i][1] . " '" . $find[$i][2] . "'";
                        }
                    } else {
                        $this->is_error = true;
                        $this->error = "UPDATE ERROR";
                    }
                }
                if (!(@mysql_query($query)))
                {
                    $this->is_error = true;
                    $this->error = "UPDATE ERROR | ".mysql_error();
                }
            }
        }
        return $this;
    }
    
    /*
    *   RETURNS ALL DATA FROM SELECTED TABLE IN ARRAY FORM
    *
    *   param: sortParams string | array
    *          desc boolean
    */
    function all($sortParams = "", $desc = false)
    {
        // execution condition checking
        if( (!($this->table == "")) && ($this->status) && (!($this->is_error)) )
        {
            if($this->cols == "")
            {
                $query="SELECT * FROM " . $this->table;
            } else {
                $query="SELECT " . $this->cols . " FROM " . $this->table;
            }
            
            // if user needs sorting by one column
            if ( is_string($sortParams) && (is_bool($desc)) && (!($sortParams=="")) )
            {
                if( ($desc) )
                {
                    $query.=" ORDER BY " . $sortParams . " DESC";
                } else {
                    $query.=" ORDER BY " . $sortParams;
                }
                
            }
            
            // if user needs sorting by several columns
            if( (is_array($sortParams)) )
            {
                // variable data type declaration
                $params = array(); /* contains column name */
                $sDir = array();   /* contains sorting direction (ASC or DESC) */
                foreach($sortParams as $key => $value)
                {
                    $params[] = $key;
                    $sDir[] = $value;
                }
                
                $query.=" ORDER BY ";
                
                for($i=0; $i<count($params)-1; $i++) $query.=$params[$i] . " " . $sDir[$i] . ", ";
                $query.=$params[count($params)-1] . " " . $sDir[count($sDir)-1];
            }
            
            // limits returning records if needed
            if(!($this->limitCount == 0))
            {
                if($this->limitStart == 0)
                {
                    $query.=" LIMIT " . $this->limitCount;
                } else {
                    $query.=" LIMIT " . $this->limitStart . "," . $this->limitCount;
                }
            }
            
            if (!($result = @mysql_query($query)))
            {
                $this->is_error = true;
                $this->error = "GET ALL ERROR | ".mysql_error();
                
                // clearing limit
                $this->limitStart = 0;
                $this->limitCount = 0;
                
                $this->cols = "";
                
                return false;
            } else {
                // clearing limit
                $this->limitStart = 0;
                $this->limitCount = 0;
                
                $this->cols = "";
                
                $arr = array();
                while($row=mysql_fetch_assoc($result)){
                    $arr[]=$row;
                }
                return $arr;
            }
        }
        
        //if somwhere before was an error it returns false
        if ($this->is_error)
        {
            return false;
        }
    }
    
    /*
    *   RETURNS DATA FROM SELECTED TABLE WITH ENTERED SPECIFICATIONS
    *
    *   param: getParams array
    *          sortParams array / string
    *          desc boolean
    */
    function get($getParams, $sortParams = "", $desc = false)
    {
        // execution condition checking
        if( (is_array($getParams)) && (!($this->table == "")) && ($this->status) && (!($this->is_error)) )
        {
            if($this->cols == "")
            {
                $query = "SELECT * FROM " . $this->table . " WHERE ";
            } else {
                $query = "SELECT " . $this->cols . " FROM " . $this->table . " WHERE ";
            }
            
            // user's getting settings
            for($i=0; $i<count($getParams); $i++)
            {
                if( (is_array($getParams[$i])) && (count($getParams[$i]) == 3) )
                {
                    if(!($i == count($getParams)-1))
                    {
                        $query.= $getParams[$i][0] . " " . $getParams[$i][1] . " '" . $getParams[$i][2] . "' AND ";
                    } else {
                        $query.= $getParams[$i][0] . " " . $getParams[$i][1] . " '" . $getParams[$i][2] . "'";
                    }
                } else {
                    $this->is_error = true;
                    $this->error = "GET DATA ERROR";
                    
                    return false;
                }
            }
            
            // if user needs sorting by one column
            if ( is_string($sortParams) && (is_bool($desc)) && (!($sortParams=="")) )
            {
                if( ($desc) )
                {
                    $query.=" ORDER BY " . $sortParams . " DESC";
                } else {
                    $query.=" ORDER BY " . $sortParams;
                }
                
            }
            
            // if user needs sorting by several columns
            if( (is_array($sortParams)) )
            {
                // variable data type declaration
                $params = array(); /* contains column name */
                $sDir = array();   /* contains sorting direction (ASC or DESC) */
                
                foreach($sortParams as $key => $value)
                {
                    $params[] = $key;
                    $sDir[] = $value;
                }
                
                $query.=" ORDER BY ";
                
                for($i=0; $i<count($params)-1; $i++) $query.=$params[$i] . " " . $sDir[$i] . ", ";
                $query.=$params[count($params)-1] . " " . $sDir[count($sDir)-1];
            }
            
            // limits returning records if needed
            if(!($this->limitCount == 0))
            {
                if($this->limitStart == 0)
                {
                    $query.=" LIMIT " . $this->limitCount;
                } else {
                    $query.=" LIMIT " . $this->limitStart . "," . $this->limitCount;
                }
            }
            
            if (!($result = @mysql_query($query)))
            {
                $this->is_error = true;
                $this->error = "GET ERROR | ".mysql_error();
                
                // clearing limit
                $this->limitStart = 0;
                $this->limitCount = 0;
                
                $this->cols = "";
                
                return false;
            } else {
                // clearing limit
                $this->limitStart = 0;
                $this->limitCount = 0;
                
                $this->cols = "";
                
                $arr = array();
                while($row=mysql_fetch_assoc($result)){
                    $arr[]=$row;
                }
                return $arr;
            }
        }

        //if somwhere before was an error it returns false
        if ($this->is_error)
        {
            return false;
        }
    }
    
    /*
    *   RETURNS ROW WHERE ID IS EQUAL WITH ENTERED OR FALSE
    *
    *   param: idNumber integer
    */
    function id($idNumber)
    {
        // execution condition checking
        if( (is_int($idNumber)) && (!($this->table == "")) && ($this->status) && (!($this->is_error)) )
        {
            if($this->cols == "")
            {
                $query = "SELECT * FROM " . $this->table . " WHERE ";
            } else {
                $query = "SELECT " . $this->cols . " FROM " . $this->table . " WHERE ";
            }
            
            $arr = array("id", "ID", "Id", "iD");
            $ind = 0;
            $bufer = $query . $arr[$ind] . " = " . $idNumber;
            while(!($result=@mysql_query($bufer)))
            {
                if ($ind == 3)
                {
                    $this->is_error = true;
                    $this->error = "GET BY ID ERROR";
                    
                    return false;
                }
                $ind++;
                $bufer = $query . $arr[$ind] . " = " . $idNumber;
            }
            
            $this->cols="";
                
            $array = array();
            while($row=mysql_fetch_assoc($result)){
                $array[]=$row;
            }
            return $array;        
        }
        
        //if somwhere before was an error it returns false
        if ($this->is_error)
        {
            return false;
        }
    }
    
    /*
    *   DELETES RECORD FROM TABLE
    *
    *   param: find int/array
    */
    function delete($find = "")
    {
        // execution condition checking
        if( (!($this->table == "")) && ($this->status) && (!($this->is_error)) )
        {
            $query = "DELETE FROM " . $this->table;
            
            // if user needs to delete every record in table
            if( $find == "" )
            {
                if(!(@mysql_query($query)))
                {
                    $this->is_error = true;
                    $this->error = "DELETE ERROR | ".mysql_error();
                }
            }
            
            // if user needs to delete record with selected id
            if( is_int($find) )
            {
                $query.=" WHERE ";
                $arr = array("id", "ID", "Id", "iD");
                $ind = 0;
                $bufer = $query . $arr[$ind] . " = " . $find;
                while(!(@mysql_query($bufer)))
                {
                    if ($ind == 3)
                    {
                        $this->is_error = true;
                        $this->error = "DELETE ERROR";
                    }
                    $ind++;
                    $bufer = $query . $arr[$ind] . " = " . $find;
                }  
            }
            
            // deleting several records
            if( is_array($find) )
            {
                $query.=" WHERE ";
                for($i=0; $i<count($find); $i++)
                {
                    if( (is_array($find[$i])) && (count($find[$i]) == 3) )
                    {
                        if(!($i == count($find)-1))
                        {
                            $query.= $find[$i][0] . " " . $find[$i][1] . " '" . $find[$i][2] . "' AND ";
                        } else {
                            $query.= $find[$i][0] . " " . $find[$i][1] . " '" . $find[$i][2] . "'";
                        }
                    } else {
                        $this->is_error = true;
                        $this->error = "DELETE ERROR";
                    }
                }
                if(!(@mysql_query($query)))
                {
                    $this->is_error = true;
                    $this->error = "DELETE ERROR | ".mysql_error();
                }
            }
        }
        return $this;
    }
    
    /*
    *   RETURNS MAX VALUE IN SELECTED COLUMN
    *
    *   param: colName string
    */
    function max($colName)
    {
        // execution condition checking
        if( (is_string($colName)) && (!($this->table == "")) && ($this->status) && (!($this->is_error)) )
        {
            $query="SELECT MAX(" . $colName . ") as max FROM " . $this->table;
            if($result = @mysql_query($query))
            {
                $row = @mysql_fetch_assoc($result);
                return $row["max"];
            } else {
                $this->is_error = true;
                $this->error = "GET MAX VALUE ERROR | ".mysql_error();
                return false;
            }
        }
        
        //if somwhere before was an error it returns false
        if ($this->is_error)
        {
            return false;
        }
    }
    
    /*
    *   RETURNS MIN VALUE IN SELECTED COLUMN
    *
    *   param: colName string
    */
    function min($colName)
    {
        // execution condition checking
        if( (is_string($colName)) && (!($this->table == "")) && ($this->status) && (!($this->is_error)) )
        {
            $query="SELECT MIN(" . $colName . ") as min FROM " . $this->table;
            if($result = @mysql_query($query))
            {
                $row = @mysql_fetch_assoc($result);
                return $row["min"];
            } else {
                $this->is_error = true;
                $this->error = "GET MIN VALUE ERROR | ".mysql_error();
                return false;
            }
        }
        
        //if somwhere before was an error it returns false
        if ($this->is_error)
        {
            return false;
        }
    }
}

?>