CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture',
  coverImg VARCHAR(255)
) default charset utf8 COMMENT '';

DROP table IF EXISTS vaults;

-- NOTE kept count is gotten when getting a keep look at restaraut repo for reference
CREATE TABLE IF NOT EXISTS keeps(
  id INT AUTO_INCREMENT PRIMARY Key,
  creatorId VARCHAR(255) NOT NULL,
  name VARCHAR(255) NOT NULL,
  description VARCHAR(1000) NOT NUll,
  img VARCHAR(255) NOT NULL,
  views INT NOT NULL DEFAULT 0,

  Foreign Key (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
)default charset utf8 COMMENT '';

CREATE TABLE IF NOT EXISTS vaults(
  id int AUTO_INCREMENT PRIMARY KEY,
  creatorId VARCHAR(255) NOT NUll,
  name VARCHAR(255) NOT NULL,
  description VARCHAR(1000) NOT NULL,
  img VARCHAR(255) NOT NULL,
  isPrivate BOOLEAN NOT NULL DEFAULT false,

  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
)default charset utf8 COMMENT '';

CREATE TABLE IF NOT EXISTS vaultkeeps(
  id INt AUTO_INCREMENT PRIMARY KEY,
  creatorId VARCHAR(255) NOT NUll,
  vaultId INT NOT NUll,
  keepId INT NOT NUll NOT NUll,


  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE,
  FOREIGN KEY (vaultId) REFERENCES vaults(id) ON DELETE CASCADE,
  FOREIGN KEY (keepId) REFERENCES keeps(id) ON DELETE CASCADE
)default charset utf8 COMMENT '';

SELECT
vaultkeeps.*,
keeps.*
-- accounts.*
FROM
vaultkeeps
JOIN keeps ON vaultkeeps.keepId = keeps.id
-- JOIN accounts ON keeps.id = accounts.id
WHERE vaultkeeps.vaultId = 1;

SELECT
vaultkeeps.*,
keeps.*,
accounts.*
FROM vaultkeeps
JOIN keeps ON vaultkeeps.keepId = keeps.id
JOIN accounts ON keeps.creatorId = accounts.id
WHERE vaultkeeps.vaultId = 12
;

SELECT
        keeps.*,
        creator.*,
        COUNT(vaultkeeps.id) AS Kept
        FROM keeps
        LEFT JOIN vaultkeeps ON vaultkeeps.keepId = keeps.id
        JOIN accounts creator ON keeps.creatorId = creator.id
        WHERE keeps.id = 35 
        GROUP BY (keeps.id);
