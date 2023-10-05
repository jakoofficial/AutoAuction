CREATE PROCEDURE GetBidHistory @Username varchar(max)
AS
SELECT auction.vehicleId, Auction.seller, BidHistory.Username, BidHistory.bidAmount, Auction.standingBid, Auction.active
FROM BidHistory
INNER JOIN Auction ON BidHistory.auctionId = Auction.auctionId
WHERE BidHistory.Username = @Username