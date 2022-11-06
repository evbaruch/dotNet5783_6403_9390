﻿namespace DO;

public struct Order
{
    public int ID { set; get; }
    public string? CustomerName { set; get; }
    public string? CustomerEmail { set; get; }
    public string? CstomerAddress { set; get; }
    public DateTime? OrderDate { set; get; }
    public DateTime? ShipDate { set; get; }
    public DateTime? DeliveryDate { set; get; }

    public override string ToString() => $@"
    ID: {ID}
    CustomerName: {CustomerName} 
    CustomerEmail: {CustomerEmail} 
    CstomerAddress: {CstomerAddress}
    DeliveryDate: {DeliveryDate}
    ";
};