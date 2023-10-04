use AutoAuction
GO

DROP PROCEDURE CreateAuction
GO

CREATE PROCEDURE CreateAuction 
@minimumPrice decimal(18,2),
@standingBid decimal(18,2), @vehicleId int, @seller varchar(max), @buyer varchar(max), @active bit
AS INSERT INTO dbo.Auction
(	minimumPrice, 
	standingBid,
	vehicleId,
	seller,
	buyer,
	active
)
VALUES (
	@minimumPrice,
	@standingBid, @vehicleId,
	@seller, @buyer,
	@active
)

SELECT SCOPE_IDENTITY() as aId