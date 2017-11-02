Select P.*, V.* from Parkings P
left join ParkingVehicles PV on P.Id = PV.ParkingId
left join Vehicles V on V.Id = PV.VehicleId
Where  p.Id = 1 and V.CheckOut is null

SELECT 
    [Extent1].[Id] AS [Id], 
    [Extent1].[ParkingNo] AS [ParkingNo], 
    [Extent1].[GarageId] AS [GarageId], 
    [Extent2].[Id] AS [Id1], 
    [Extent2].[VehicleId] AS [VehicleId], 
    [Extent2].[ParkingId] AS [ParkingId]
    FROM  [dbo].[Parkings] AS [Extent1]
    INNER JOIN [dbo].[ParkingVehicles] AS [Extent2] ON [Extent1].[Id] = [Extent2].[ParkingId]