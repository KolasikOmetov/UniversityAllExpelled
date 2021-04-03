using TypographyShopBusinessLogic.BindingModels;
using TypographyShopBusinessLogic.Interfaces;
using TypographyShopBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using TypographyShopDatabaseImplement.Models;

namespace TypographyShopDatabaseImplement.Implements
{
    public class OrderStorage : IOrderStorage
    {
        public List<OrderViewModel> GetFullList()
        {
            using (var context = new UniversityAllExpelledWarehouserViewDatabase())
            {
                return context.Orders
                .Select(rec => new OrderViewModel
                {
                    Id = rec.Id,
                    PrintedId = rec.PrintedId,
                    PrintedName = context.Printeds.FirstOrDefault(pr => pr.Id == rec.PrintedId).PrintedName,
                    Count = rec.Count,
                    Sum = rec.Sum,
                    Status = rec.Status,
                    DateCreate = rec.DateCreate,
                    DateImplement = rec.DateImplement,
                })
                .ToList();
            }
        }
        public List<OrderViewModel> GetFilteredList(OrderBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new UniversityAllExpelledWarehouserViewDatabase())
            {
                return context.Orders
                .Where(rec => rec.PrintedId == model.PrintedId)
                .Select(rec => new OrderViewModel
                {
                    Id = rec.Id,
                    PrintedId = rec.PrintedId,
                    PrintedName = context.Printeds.FirstOrDefault(pr => pr.Id == rec.PrintedId).PrintedName,
                    Count = rec.Count,
                    Sum = rec.Sum,
                    Status = rec.Status,
                    DateCreate = rec.DateCreate,
                    DateImplement = rec.DateImplement,
                })
                .ToList();
            }
        }
        public OrderViewModel GetElement(OrderBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new UniversityAllExpelledWarehouserViewDatabase())
            {
                var order = context.Orders
                .FirstOrDefault(rec => rec.Id == model.Id);
                return order != null ?
                new OrderViewModel
                {
                    Id = order.Id,
                    PrintedId = order.PrintedId,
                    PrintedName = context.Printeds.FirstOrDefault(rec => rec.Id == order.PrintedId)?.PrintedName,
                    Count = order.Count,
                    Sum = order.Sum,
                    Status = order.Status,
                    DateCreate = order.DateCreate,
                    DateImplement = order.DateImplement,
                } :
                null;
            }
        }
        public void Insert(OrderBindingModel model)
        {
            using (var context = new UniversityAllExpelledWarehouserViewDatabase())
            {
                Order order = new Order
                {
                    PrintedId = model.PrintedId,
                    Count = model.Count,
                    Sum = model.Sum,
                    Status = model.Status,
                    DateCreate = model.DateCreate,
                    DateImplement = model.DateImplement,
                };
                context.Orders.Add(order);
                context.SaveChanges();
                CreateModel(model, order);
                context.SaveChanges();
            }
        }
        public void Update(OrderBindingModel model)
        {
            using (var context = new UniversityAllExpelledWarehouserViewDatabase())
            {
                var element = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
                element.PrintedId = model.PrintedId;
                element.Count = model.Count;
                element.Sum = model.Sum;
                element.Status = model.Status;
                element.DateCreate = model.DateCreate;
                element.DateImplement = model.DateImplement;
                CreateModel(model, element);
                context.SaveChanges();
            }
        }
        public void Delete(OrderBindingModel model)
        {
            using (var context = new UniversityAllExpelledWarehouserViewDatabase())
            {
                Order element = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);
                if (element != null)
                {
                    context.Orders.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }
        private Order CreateModel(OrderBindingModel model, Order order)
        {
            if (model == null)
            {
                return null;
            }

            using (var context = new UniversityAllExpelledWarehouserViewDatabase())
            {
                Printed element = context.Printeds.FirstOrDefault(rec => rec.Id == model.PrintedId);
                if (element != null)
                {
                    if(element.Orders == null)
                    {
                        element.Orders = new List<Order>();
                    }
                    element.Orders.Add(order);
                    context.Printeds.Update(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
            return order;
        }
    }
}