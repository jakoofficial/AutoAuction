CREATE PROCEDURE UpdateAuction @aID int, @buyerName varchar(300), @bid decimal(18, 2)
AS UPDATE Auction
SET buyer = @buyerName, standingBid = @bid
WHERE Auction.auctionId = @aID