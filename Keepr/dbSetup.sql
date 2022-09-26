
DROP TABLE keeps;
DROP TABLE vaults;
DROP TABLE vaultKeeps;



-- SECTION Account
CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';


-- SECTION Keeps
CREATE TABLE IF NOT EXISTS keeps(
    id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
    updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
    creatorId VARCHAR(255) NOT NULL,
    name VARCHAR(255) NOT NULL,
    description TEXT NOT NULL,
    img VARCHAR(255) NOT NULL,
    views INT NOT NULL DEFAULT 0,
    kept INT NOT NULL DEFAULT 0,

  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';

INSERT INTO keeps
(CreatorId, name, img, description)
VALUES
('63237a3deb908c6c2763fd4a', 'Futurama', 'https://cdni.russiatoday.com/rbthmedia/images/web/en-rbth/images/2014-07/big/Futurama-468.jpg', 'Russian artist recreates Futurama landscape in 3D');


-- SECTION Vaults
CREATE TABLE IF NOT EXISTS vaults(
    id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
    updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
    creatorId VARCHAR(255) NOT NULL,
    name VARCHAR(255) NOT NULL,
    description TEXT NOT NULL,
    img VARCHAR(255) NOT NULL,
    isPrivate BOOL NOT NULL DEFAULT false,

  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';

INSERT INTO vaults
(CreatorId, name, img, description, isPrivate)
VALUES
('63237a3deb908c6c2763fd4a', 'Futurama', 'https://live.staticflickr.com/65535/50933783533_afa31e47e8_z.jpg', 'Vault for all things related to Futurama', 1);



-- SECTION VaultKeeps

CREATE TABLE vaultKeeps(
    id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
    updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
    creatorId VARCHAR(255) NOT NULL,
    vaultId INT NOT NULL,
    keepId INT NOT NULL,

  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE,
  FOREIGN KEY (vaultId) REFERENCES vaults(id) ON DELETE CASCADE,
  FOREIGN KEY (keepId) REFERENCES keeps(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';

INSERT INTO vaultKeeps
(CreatorId, VaultId, KeepId)
VALUES
('63237a3deb908c6c2763fd4a', 1, 1);




-- Testing ground

      SELECT 
        k.*,
        a.*
        FROM keeps k
        JOIN accounts a ON a.id = k.creatorId
        WHERE a.id = '630848238b092f13fbc505c6';

              SELECT 
        v.*
        FROM vaults v
        WHERE v.creatorId = '630848238b092f13fbc505c6'