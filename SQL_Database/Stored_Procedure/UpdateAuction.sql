CREATE PROCEDURE UpdateAuction @aID int, @buyerName varchar(300), @bid decimal(18, 2), @activeStage bit
AS UPDATE Auction
SET buyer = @buyerName, standingBid = @bid,
active = @activeStage
WHERE Auction.auctionId = @aID