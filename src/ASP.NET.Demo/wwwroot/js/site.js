﻿$(document).ready(function () {
    $(".dropdown-toggle").dropdown();

    var isSymbolsLoaded = false;

    $('#accountChangeModal').on('shown.bs.modal', function () {
        if (isSymbolsLoaded) {
            isSymbolsLoaded = false;
            $('#accountChangeModal').modal('hide');
        }
    })

    $(function () {
        $("#accounts-list").on("change", function () {
            isSymbolsLoaded = false;

            $("#accountChangeModal").modal({
                backdrop: 'static',
                keyboard: false
            });
            $('#accountChangeModal').modal('toggle')
            var accountLogin = $(this).val();
            $.get(`?handler=AccountChanged&AccountLogin=${accountLogin}`).done(() => {
                $.get(`?handler=Symbols&AccountLogin=${accountLogin}`).done(function (symbols) {
                    var rows = '';
                    $.each(symbols, function (i, symbol) {
                        var item = `<tr id="${symbol.id}">
                            <td>${symbol.name}</td>
                            <td>${symbol.bid}</td>
                            <td>${symbol.ask}</td></tr>`;

                        rows += item;
                    });
                    $('#symbols-table-body').html(rows);

                    isSymbolsLoaded = true;

                    $('#accountChangeModal').modal('hide');
                });
            });
        });
    });
});