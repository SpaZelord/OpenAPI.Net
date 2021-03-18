﻿using Prism.Commands;
using Prism.Services.Dialogs;
using System.Collections.Generic;
using Trading.UI.Demo.Models;
using Trading.UI.Demo.Services;

namespace Trading.UI.Demo.ViewModels
{
    public class CreateModifyOrderViewModel : DialogAwareViewBase
    {
        private bool _isModifyingMarketOrder;
        private bool _isModifyingPendingOrder;
        private MarketOrderModel _marketOrderModel;
        private PendingOrderModel _pendingOrderModel;
        private List<SymbolModel> _symbols;
        private readonly IApiService _apiService;
        private AccountModel _account;

        public CreateModifyOrderViewModel(IApiService apiService)
        {
            _apiService = apiService;

            PlaceMarketOrderCommand = new DelegateCommand(PlaceMarketOrder);

            ModifyMarketOrderCommand = new DelegateCommand(ModifyMarketOrder);

            PlacePendingOrderCommand = new DelegateCommand(PlacePendingOrder);

            ModifyPendingOrderCommand = new DelegateCommand(ModifyPendingOrder);
        }

        public DelegateCommand PlaceMarketOrderCommand { get; }

        public DelegateCommand ModifyMarketOrderCommand { get; }

        public DelegateCommand PlacePendingOrderCommand { get; }

        public DelegateCommand ModifyPendingOrderCommand { get; }

        public bool IsModifyingMarketOrder { get => _isModifyingMarketOrder; set => SetProperty(ref _isModifyingMarketOrder, value); }

        public bool IsModifyingPendingOrder { get => _isModifyingPendingOrder; set => SetProperty(ref _isModifyingPendingOrder, value); }

        public MarketOrderModel MarketOrderModel { get => _marketOrderModel; set => SetProperty(ref _marketOrderModel, value); }

        public PendingOrderModel PendingOrderModel { get => _pendingOrderModel; set => SetProperty(ref _pendingOrderModel, value); }

        public List<SymbolModel> Symbols { get => _symbols; set => SetProperty(ref _symbols, value); }

        public override void OnDialogClosed()
        {
            MarketOrderModel = null;
            PendingOrderModel = null;

            IsModifyingMarketOrder = false;
            IsModifyingPendingOrder = false;

            Symbols = null;

            _account = null;
        }

        public override void OnDialogOpened(IDialogParameters parameters)
        {
            if (parameters.TryGetValue("Account", out _account))
            {
                Symbols = new List<SymbolModel>(_account.Symbols);
            }

            if (parameters.TryGetValue<MarketOrderModel>("MarketOrder", out var marketOrderModel))
            {
                Title = "Modify Market Order";

                MarketOrderModel = marketOrderModel;

                IsModifyingMarketOrder = true;
            }
            else if (parameters.TryGetValue<PendingOrderModel>("PendingOrder", out var pendingOrderModel))
            {
                Title = "Modify Pending Order";

                PendingOrderModel = pendingOrderModel;

                IsModifyingPendingOrder = true;
            }
            else
            {
                Title = "Create New Order";

                MarketOrderModel = new MarketOrderModel();
                PendingOrderModel = new PendingOrderModel();
            }
        }

        private async void PlaceMarketOrder()
        {
            try
            {
                await _apiService.CreateNewOrder(MarketOrderModel, _account.Id, _account.IsLive);
            }
            finally
            {
                OnRequestClose(new DialogResult(ButtonResult.OK));
            }
        }

        private void ModifyMarketOrder()
        {
            OnRequestClose(new DialogResult(ButtonResult.OK));
        }

        private void ModifyPendingOrder()
        {
            OnRequestClose(new DialogResult(ButtonResult.OK));
        }

        private async void PlacePendingOrder()
        {
            try
            {
                await _apiService.CreateNewOrder(PendingOrderModel, _account.Id, _account.IsLive);
            }
            finally
            {
                OnRequestClose(new DialogResult(ButtonResult.OK));
            }
        }
    }
}