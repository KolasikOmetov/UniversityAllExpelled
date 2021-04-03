using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.Enums;
using UniversityBusinessLogic.Interfaces;
using UniversityBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;

namespace UniversityBusinessLogic.BusinessLogics
{
	public class OrderLogic
	{
		private readonly IOrderStorage _orderStorage;
		public OrderLogic(IOrderStorage orderStorage)
		{
			_orderStorage = orderStorage;
		}
		public List<OrderViewModel> Read(OrderBindingModel model)
		{
			if (model == null)
			{
				return _orderStorage.GetFullList();
			}
			if (model.Id.HasValue)
			{
				return new List<OrderViewModel> { _orderStorage.GetElement(model) };
			}
			return _orderStorage.GetFilteredList(model);
		}
		public void CreateOrder(CreateOrderBindingModel model)
		{
			_orderStorage.Insert(new OrderBindingModel
			{
				PrintedId = model.PrintedId,
				Count = model.Count,
				Sum = model.Sum,
				DateCreate = DateTime.Now,
				Status = OrderStatus.Принят
			});
		}
		public void TakeOrderInWork(ChangeStatusBindingModel model)
		{
			var order = _orderStorage.GetElement(new OrderBindingModel { Id = model.OrderId });
			if (order == null)
			{
				throw new Exception("Не найден заказ");
			}
			if (order.Status != OrderStatus.Принят)
			{
				throw new Exception("Заказ не в статусе \"Принят\"");
			}
			_orderStorage.Update(new OrderBindingModel
			{
				Id = order.Id,
				PrintedId = order.PrintedId,
				Count = order.Count,
				Sum = order.Sum,
				DateCreate = order.DateCreate,
				Status = OrderStatus.Выполняется
			});
		}
		public void FinishOrder(ChangeStatusBindingModel model)
		{
			var order = _orderStorage.GetElement(new OrderBindingModel { Id = model.OrderId });
			if (order == null)
			{
				throw new Exception("Не найден заказ");
			}
			if (order.Status != OrderStatus.Выполняется)
			{
				throw new Exception("Заказ не в статусе \"Выполняется\"");
			}
			_orderStorage.Update(new OrderBindingModel
			{
				Id = order.Id,
				PrintedId = order.PrintedId,
				Count = order.Count,
				Sum = order.Sum,
				DateCreate = order.DateCreate,
				DateImplement = DateTime.Now,
				Status = OrderStatus.Готов
			});
		}
		public void PayOrder(ChangeStatusBindingModel model)
		{
			var order = _orderStorage.GetElement(new OrderBindingModel { Id = model.OrderId });
			if (order == null)
			{
				throw new Exception("Не найден заказ");
			}
			if (order.Status != OrderStatus.Готов)
			{
				throw new Exception("Заказ не в статусе \"Готов\"");
			}
			_orderStorage.Update(new OrderBindingModel
			{
				Id = order.Id,
				PrintedId = order.PrintedId,
				Count = order.Count,
				Sum = order.Sum,
				DateCreate = order.DateCreate,
				DateImplement = order.DateImplement,
				Status = OrderStatus.Оплачен
			});
		}
	}
}
