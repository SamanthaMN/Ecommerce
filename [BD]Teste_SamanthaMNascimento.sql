CREATE database EcommerceTeste;

USE EcommerceTeste;
/*-----------------------------------------------TABELAS-----------------------------------------------*/

CREATE TABLE TipoPessoa(
	ID 			INT Auto_Increment 	PRIMARY KEY,
	Descricao 	Varchar(150) 		NOT NULL
);

CREATE TABLE Genero(
	ID 			INT Auto_Increment PRIMARY KEY,
	Descricao 	Varchar(150) NOT NULL
);

CREATE TABLE Clientes(
	ID 							INT Auto_Increment PRIMARY KEY,
	NomeRazaoSocial 			Varchar(150) 	NOT NULL,
	Email 						Varchar(150)  	NOT NULL,
	Telefone 					BIGINT(11) 		NOT NULL,
	DataCadastro 				DateTime,
	TipoPessoa 					INT 			NOT NULL,
	CpfCnpj 					Varchar(14) 	NOT NULL,
	InscricaoEstadual 			Varchar(12) 	NOT NULL,
	Isento_InscricaoEstadual 	TINYINT(1),
	IdGenero 					INT 			NOT NULL,
	DataNascimento 				DateTime 		NOT NULL,
	Bloqueado 					TINYINT(1),
	Senha 						Varchar(15)  	NOT NULL
);
/*-----------------------------------------------PROCEDURES-----------------------------------------------*/
DELIMITER //
CREATE PROCEDURE Ecommerce_ClienteInserir(
    IN p_NomeRazaoSocial 				Varchar(150),
	IN p_Email 						Varchar(150),
	IN p_Telefone 					BIGINT,
	IN p_DataCadastro 				DateTime,
	IN p_TipoPessoa 				INT,
	IN p_CpfCnpj 					Varchar(14),
	IN p_InscricaoEstadual 			Varchar(12),
	IN p_Isento_InscricaoEstadual 	TINYINT(1),    
	IN p_IdGenero 					INT,
	IN p_DataNascimento 			DateTime,
	IN p_Bloqueado 					TINYINT(1),
	IN p_Senha 						Varchar(15)
)
BEGIN

	IF EXISTS (SELECT 1 FROM Clientes WHERE Email = p_Email) THEN
		SIGNAL SQLSTATE '45000' SET Message_text = 'Este e-mail já está cadastrado para outro Cliente';
			
	ELSEIF EXISTS(SELECT 1 FROM Clientes WHERE CpfCnpj = p_CpfCnpj) THEN
		SIGNAL SQLSTATE '45000' SET Message_text = 'Este CPF/CNPJ já está cadastrado para outro Cliente';
	
	ELSEIF EXISTS(SELECT 1 FROM Clientes WHERE InscricaoEstadual = p_InscricaoEstadual) THEN
		SIGNAL SQLSTATE '45000' SET Message_text =  'Este Inscrição Estadual já está cadastrado para outro Cliente';
	ELSE
		INSERT INTO Clientes( 	NomeRazaoSocial, 
								Email, 
								Telefone, 
								DataCadastro, 
								TipoPessoa, 
								CpfCnpj, 
								InscricaoEstadual, 
								Isento_InscricaoEstadual, 
								IdGenero, 
								DataNascimento, 
								Bloqueado, 
								Senha 	
							)
		VALUES (p_NomeRazaoSocial, p_Email, p_Telefone, p_DataCadastro, p_TipoPessoa, p_CpfCnpj, 
				p_InscricaoEstadual, p_Isento_InscricaoEstadual, p_IdGenero, p_DataNascimento, p_Bloqueado, p_Senha);
	END IF;
END;
//

DELIMITER //
CREATE PROCEDURE Ecommerce_ClienteSelecionarPorId(
    IN p_IdCliente 				INT
)
BEGIN
	SELECT
		ID, 
		NomeRazaoSocial, 
		Email, 
		Telefone, 
		DataCadastro, 
		TipoPessoa, 
		CpfCnpj, 
		InscricaoEstadual, 
		Isento_InscricaoEstadual, 
		IdGenero, 
		DataNascimento, 
		Bloqueado, 
		Senha 						
	FROM Clientes
	WHERE ID = p_IdCliente;
END;
//

DELIMITER //
CREATE PROCEDURE Ecommerce_ClienteListar(
    IN p_Limite 				INT,
    IN p_Pagina 				INT,
    IN p_Ordenacao 				VARCHAR(255),
	IN p_OrdenacaoCampo 		VARCHAR(255),
	IN p_FiltroNomeRazaoSocial  VARCHAR(255),
	IN p_FiltroEmail  			VARCHAR(255),
	IN p_FiltroTelefone  		BIGINT(11),
	IN p_FiltroDataCadastro  	DATETIME,
	IN p_FiltroBloqueado	  	TINYINT(1),
	OUT P_total INT
)
BEGIN
	DECLARE v_calculoPagLimite int;
	SET v_calculoPagLimite = (p_Pagina - 1) * p_Limite;
	SET @s = CONCAT(
						'SELECT SQL_CALC_FOUND_ROWS NomeRazaoSocial,Email,Telefone,DataCadastro,Bloqueado FROM Clientes ',
						'WHERE (NomeRazaoSocial IS NULL OR NomeRazaoSocial LIKE ''%', p_FiltroNomeRazaoSocial ,'%'') ',
						'AND (Email IS NULL 				OR Email 		LIKE ''%', p_FiltroEmail ,'%'') ',
						'AND (Telefone IS NULL 				OR Telefone 	LIKE ''%', p_FiltroTelefone ,'%'') ',
						'AND (DataCadastro IS NULL 			OR DataCadastro = ''', p_FiltroDataCadastro ,''') ',
						'AND (Bloqueado IS NULL 			OR Bloqueado	= ''', p_FiltroBloqueado ,''') ',
						'Order BY ' , p_OrdenacaoCampo, ',' , p_Ordenacao,
						' Limit ', v_calculoPagLimite, ',', p_Limite
					);
	PREPARE STMT FROM @s;
	EXECUTE STMT;
	DEALLOCATE Prepare STMT;
	
	SELECT Found_Rows() Into P_total;
END;
//