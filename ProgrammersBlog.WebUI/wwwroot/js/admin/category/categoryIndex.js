$(document).ready(function () {
    const categoriesTable = $('#categoriesTable');
    categoriesTable.DataTable({
        dom: "<'row'<'col-sm-3'l><'col-sm-6 text-center'B><'col-sm-3'f>>" +
            "<'row'<'col-sm-12'tr>>" +
            "<'row'<'col-sm-5'i><'col-sm-7'p>>",
        buttons: [
            {
                text: 'Ekle',
                attr: {
                    id: 'btnAdd'
                },
                className: 'btn btn-sm btn-success',
                action: function (e, dt, node, config) {

                }
            },
            {
                text: 'Yenile',
                className: 'btn btn-sm btn-danger',
                action: function (e, dt, node, config) {
                    $.ajax({
                        type: 'GET',
                        url: '/Admin/Category/GetAllCategories',
                        dataType: 'JSON',
                        contentType: 'application/json',
                        beforeSend: function () {
                            $('#categoriesTable').hide();
                            $('.spinner-border').show();
                        },
                        success: function (response) {
                            const categoryListDto = JSON.parse(response);
                            if (categoryListDto.ResultStatus === 0) {
                                let tableBody = '';
                                $.each(categoryListDto.Categories.$values, function (index, category) {
                                    tableBody +=
                                        `
                                            <tr>
                                                <td>${category.Id}</td>
                                                <td>${category.Name}</td>
                                                <td>${category.Description}</td>
                                                <td>
                                                    ${category.IsActive ? ' <span class="badge badge-success" > Aktif </span>' : ' <span class="badge badge-danger" > Pasif </span>'}
                                                </td>
                                                <td>
                                                    ${category.IsDeleted ? '<span class="badge badge-danger" > Evet </span>' : '<span class="badge badge-success" > Hayır </span>'}
                                                </td>
                                                <td>${convertToShortDate(category.CreatedDate)}</td>
                                                <td>${category.CreatedByName}</td>
                                                <td>
                                                    <button class="btn btn-sm btn-primary btn-edit" data-id="${category.Id}"><span class="fas fa-edit"></span> Düzenle</button>
                                                    <button class="btn btn-sm btn-danger btn-delete" data-id="${category.Id}"><span class="fas fa-minus-circle"></span> Sil</button>
                                                </td>
                                            </tr>
                                          `;
                                });
                                $('#categoriesTable > tbody').replaceWith(tableBody);
                                $('.spinner-border').hide();
                                $('#categoriesTable').fadeIn(2000);
                            } else {
                                iziToast.danger({
                                    title: 'İşlem başarısız!',
                                    message: `${categoryListDto.Message}`,
                                    position: 'topCenter'
                                });
                            }
                        },
                        error: function (error) {
                            $('.spinner-border').hide();
                            $('#categoriesTable').fadeIn(1000);
                            iziToast.danger({
                                title: 'İşlem Başarısız',
                                message: error.responseText,
                                position: 'topCenter'
                            });
                        }
                    })
                }
            }
        ],
        language: {
            "emptyTable": "Tabloda herhangi bir veri mevcut değil",
            "info": "_TOTAL_ kayıttan _START_ - _END_ arasındaki kayıtlar gösteriliyor",
            "infoEmpty": "Kayıt yok",
            "infoFiltered": "(_MAX_ kayıt içerisinden bulunan)",
            "infoThousands": ".",
            "lengthMenu": "Sayfada _MENU_ kayıt göster",
            "loadingRecords": "Yükleniyor...",
            "processing": "İşleniyor...",
            "search": "Ara:",
            "zeroRecords": "Eşleşen kayıt bulunamadı",
            "paginate": {
                "first": "İlk",
                "last": "Son",
                "next": "Sonraki",
                "previous": "Önceki"
            },
            "aria": {
                "sortAscending": ": artan sütun sıralamasını aktifleştir",
                "sortDescending": ": azalan sütun sıralamasını aktifleştir"
            },
            "select": {
                "rows": {
                    "_": "%d kayıt seçildi",
                    "1": "1 kayıt seçildi",
                    "0": "-"
                },
                "0": "-",
                "1": "%d satır seçildi",
                "2": "-",
                "_": "%d satır seçildi",
                "cells": {
                    "1": "1 hücre seçildi",
                    "_": "%d hücre seçildi"
                },
                "columns": {
                    "1": "1 sütun seçildi",
                    "_": "%d sütun seçildi"
                }
            },
            "autoFill": {
                "cancel": "İptal",
                "fill": "Bütün hücreleri <i>%d<i> ile doldur<\/i><\/i>",
                "fillHorizontal": "Hücreleri yatay olarak doldur",
                "fillVertical": "Hücreleri dikey olarak doldur",
                "info": "-"
            },
            "buttons": {
                "collection": "Koleksiyon <span class=\"ui-button-icon-primary ui-icon ui-icon-triangle-1-s\"><\/span>",
                "colvis": "Sütun görünürlüğü",
                "colvisRestore": "Görünürlüğü eski haline getir",
                "copy": "Koyala",
                "copyKeys": "Tablodaki sisteminize kopyalamak için CTRL veya u2318 + C tuşlarına basınız.",
                "copySuccess": {
                    "1": "1 satır panoya kopyalandı",
                    "_": "%ds satır panoya kopyalandı"
                },
                "copyTitle": "Panoya kopyala",
                "csv": "CSV",
                "excel": "Excel",
                "pageLength": {
                    "-1": "Bütün satırları göster",
                    "1": "-",
                    "_": "%d satır göster"
                },
                "pdf": "PDF",
                "print": "Yazdır"
            },
            "decimal": "-",
            "infoPostFix": "-",
            "searchBuilder": {
                "add": "Koşul Ekle",
                "button": {
                    "0": "Arama Oluşturucu",
                    "_": "Arama Oluşturucu (%d)"
                },
                "clearAll": "Hepsini Kaldır",
                "condition": "Koşul",
                "conditions": {
                    "date": {
                        "after": "Sonra",
                        "before": "Önce",
                        "between": "Arasında",
                        "empty": "Boş",
                        "equals": "Eşittir",
                        "not": "Değildir",
                        "notBetween": "Dışında",
                        "notEmpty": "Dolu"
                    },
                    "moment": {
                        "after": "Sonra",
                        "before": "Önce",
                        "between": "Arasında",
                        "empty": "Boş",
                        "equals": "Eşittir",
                        "not": "Değildir",
                        "notBetween": "Dışında",
                        "notEmpty": "Dolu"
                    },
                    "number": {
                        "between": "Arasında",
                        "empty": "Boş",
                        "equals": "Eşittir",
                        "gt": "Büyüktür",
                        "gte": "Büyük eşittir",
                        "lt": "Küçüktür",
                        "lte": "Küçük eşittir",
                        "not": "Değildir",
                        "notBetween": "Dışında",
                        "notEmpty": "Dolu"
                    },
                    "string": {
                        "contains": "İçerir",
                        "empty": "Boş",
                        "endsWith": "İle biter",
                        "equals": "Eşittir",
                        "not": "Değildir",
                        "notEmpty": "Dolu",
                        "startsWith": "İle başlar"
                    }
                },
                "data": "Veri",
                "deleteTitle": "Filtreleme kuralını silin",
                "leftTitle": "Kriteri dışarı çıkart",
                "logicAnd": "ve",
                "logicOr": "veya",
                "rightTitle": "Kriteri içeri al",
                "title": {
                    "0": "Arama Oluşturucu",
                    "_": "Arama Oluşturucu (%d)"
                },
                "value": "Değer"
            },
            "searchPanes": {
                "clearMessage": "Hepsini Temizle",
                "collapse": {
                    "0": "Arama Bölmesi",
                    "_": "Arama Bölmesi (%d)"
                },
                "count": "{total}",
                "countFiltered": "{shown}\/{total}",
                "emptyPanes": "Arama Bölmesi yok",
                "loadMessage": "Arama Bölmeleri yükleniyor ...",
                "title": "Etkin filtreler - %d"
            },
            "searchPlaceholder": "Ara",
            "thousands": "."
        }
    });
    // Datatable end here

    // Ajax/GET _CategoryAddPartial içindeki modal form çağrılması
    $(function () {
        const url = '/Admin/Category/Add';
        const modalPlaceHolderDiv = document.getElementById('modalPlaceHolder');
        const btnAdd = document.getElementById('btnAdd');
        btnAdd.addEventListener('click', function (e) {
            $.get(url).done(function (data) {
                modalPlaceHolderDiv.innerHTML = data;
                const insideModal = modalPlaceHolderDiv.querySelectorAll(".modal")[0];
                $(insideModal).modal('show');
                $('#category-is-active-button').bootstrapSwitch();
            })
        });
        // Ajax POST / Post form data CategoryAddDTO başlangıcı
        $(modalPlaceHolderDiv).on('click', '#btnSave', function (e) {
            e.preventDefault();
            const form = $('#form-category-add');
            const actionUrl = form.attr('action');
            const dataToSend = form.serialize();
            $.post(actionUrl, dataToSend).done(function (data) {
                const categoryAddAjaxViewModel = jQuery.parseJSON(data);
                const newFormBody = $('.modal-body', categoryAddAjaxViewModel.CategoryAddPartial);
                $(modalPlaceHolderDiv).find('.modal-body').replaceWith(newFormBody);
                $('#category-is-active-button').bootstrapSwitch();
                const isValid = newFormBody.find('[name="IsValid"]').val() === 'True';
                if (isValid) {
                    $(modalPlaceHolderDiv).find('.modal').modal('hide');
                    const newTableRow =
                        `
                                <tr name="${categoryAddAjaxViewModel.CategoryDto.Category.Id}">
                                    <td>${categoryAddAjaxViewModel.CategoryDto.Category.Id}</td>
                                    <td>${categoryAddAjaxViewModel.CategoryDto.Category.Name}</td>
                                    <td>${categoryAddAjaxViewModel.CategoryDto.Category.Description}</td>
                                    <td>
                                        ${categoryAddAjaxViewModel.CategoryDto.Category.IsActive ? ' <span class="badge badge-success" > Aktif </span>' : ' <span class="badge badge-danger" > Pasif </span>'}
                                    </td>
                                    <td>
                                        ${categoryAddAjaxViewModel.CategoryDto.Category.IsDeleted ? '<span class="badge badge-danger" > Evet </span>' : '<span class="badge badge-success" > Hayır </span>'}
                                    </td>
                                    <td>${convertToShortDate(categoryAddAjaxViewModel.CategoryDto.Category.CreatedDate)}</td>
                                    <td>${categoryAddAjaxViewModel.CategoryDto.Category.CreatedByName}</td>
                                    <td>
                                        <button class="btn btn-sm btn-primary btn-edit" data-id="${categoryAddAjaxViewModel.CategoryDto.Category.Id}"><span class="fas fa-edit"></span> Düzenle</button>
                                        <button class="btn btn-sm btn-danger btn-delete" data-id="${categoryAddAjaxViewModel.CategoryDto.Category.Id}"><span class="fas fa-minus-circle"></span> Sil</button>
                                    </td>
                                </tr>
                            `;
                    const newTableRowObject = $(newTableRow);
                    newTableRowObject.hide();
                    $('#categoriesTable').append(newTableRowObject);
                    newTableRowObject.fadeIn(3500);
                    iziToast.success({
                        title: 'Başarılı',
                        message: `${categoryAddAjaxViewModel.CategoryDto.Message}`,
                        position: 'topCenter'
                    });
                } else {
                    let summaryText = '';
                    $('#validation-summary > ul > li').each(function () {
                        let text = $(this).text();
                        summaryText += `*${text}\n`;
                    });
                    iziToast.warning({
                        title: 'Başarısız!',
                        message: summaryText,
                        position: 'topCenter'
                    });
                }
            });
        });
    });

    // Ajax POST / Delete a Category
    $(document).on('click', '.btn-delete', function (e) {
        e.preventDefault();
        const id = $(this).data('id');
        const tableRow = $(`[name="${id}"]`);
        const categoryName = tableRow.find('td:eq(1)').text();
        Swal.fire({
            title: 'Silme istediğinize emin misiniz?',
            text: `${categoryName} adlı kategori silinecek. Bu işlem geri alınamaz!`,
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Evet!',
            cancelButtonText: 'Hayır'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    type: 'POST',
                    dataType: 'json',
                    url: '/Admin/Category/Delete',
                    data: {
                        categoryId: id
                    },
                    success: function (data) {
                        const categoryDto = jQuery.parseJSON(data);
                        if (categoryDto.ResultStatus === 0) {
                            Swal.fire(
                                'Silindi!',
                                `${categoryDto.Category.Name} adlı kategori başarıyla silinmiştir.`,
                                'success'
                            );

                            tableRow.fadeOut(2000);
                        } else {
                            Swal.fire({
                                icon: 'error',
                                title: 'Bir hata oluştu!',
                                text: `${categoryDto.Message}`
                            });
                        }
                    },
                    error: function (err) {
                        console.error(err);
                        iziToast.warning({
                            title: 'Hata!',
                            message: err.responseText,
                            position: 'topCenter'
                        });
                    }
                });
            }
        })
    });

    // Ajax GET / Edit a Category
    $(function () {
        const url = `/Admin/Category/Update/`;
        const placeHolderDiv = $('#modalPlaceHolder');
        $(document).on('click', '.btn-edit', function (e) {
            e.preventDefault();
            const id = $(this).attr('data-id');
            $.get(url, {categoryId: id})
                .done(function (data) {
                    placeHolderDiv.html(data);
                    placeHolderDiv.find('.modal').modal('show');
                    // $('#category-is-active-button').bootstrapSwitch();
                    // $('#category-is-delete-button').bootstrapSwitch();
                }).fail(function () {
                iziToast.warning({
                    title: 'Ooops!',
                    message: 'Bir hata oluştu',
                    position: 'topCenter'
                });
            })
        });

        // Ajax POST / Update a Category
        placeHolderDiv.on('click', '#btnUpdate', function (event) {
            event.preventDefault();
            const form = $('#form-category-update');
            const actionUrl = form.attr('action');
            const dataToSend = form.serialize();
            $.post(actionUrl, dataToSend)
                .done(function (data) {
                    const categoryUpdateAjaxModel = jQuery.parseJSON(data);
                    console.log(categoryUpdateAjaxModel);
                    const newFormBody = $('.modal-body', categoryUpdateAjaxModel.CategoryUpdatePartial);
                    placeHolderDiv.find('.modal-body').replaceWith(newFormBody);
                    const isValid = newFormBody.find('[name="IsValid"]').val() === 'True';
                    if (isValid) {
                        placeHolderDiv.find('.modal').modal('hide');
                        const newTableRow =
                            `
                                <tr name="${categoryUpdateAjaxModel.CategoryDto.Category.Id}">
                                    <td>${categoryUpdateAjaxModel.CategoryDto.Category.Id}</td>
                                    <td>${categoryUpdateAjaxModel.CategoryDto.Category.Name}</td>
                                    <td>${categoryUpdateAjaxModel.CategoryDto.Category.Description}</td>
                                    <td>
                                        ${categoryUpdateAjaxModel.CategoryDto.Category.IsActive ? ' <span class="badge badge-success" > Aktif </span>' : ' <span class="badge badge-danger" > Pasif </span>'}
                                    </td>
                                    <td>
                                        ${categoryUpdateAjaxModel.CategoryDto.Category.IsDeleted ? '<span class="badge badge-danger" > Evet </span>' : '<span class="badge badge-success" > Hayır </span>'}
                                    </td>
                                    <td>${convertToShortDate(categoryUpdateAjaxModel.CategoryDto.Category.CreatedDate)}</td>
                                    <td>${categoryUpdateAjaxModel.CategoryDto.Category.CreatedByName}</td>
                                    <td>
                                        <button class="btn btn-sm btn-primary btn-edit" data-id="${categoryUpdateAjaxModel.CategoryDto.Category.Id}"><span class="fas fa-edit"></span> Düzenle</button>
                                        <button class="btn btn-sm btn-danger btn-delete" data-id="${categoryUpdateAjaxModel.CategoryDto.Category.Id}"><span class="fas fa-minus-circle"></span> Sil</button>
                                    </td>
                                </tr>
                            `;
                        const newTableRowObject = $(newTableRow);
                        const categoryTableRow = $(`[name="${categoryUpdateAjaxModel.CategoryDto.Category.Id}"]`);
                        newTableRowObject.hide();
                        categoryTableRow.replaceWith(newTableRowObject)
                        newTableRowObject.fadeIn(3500);
                        iziToast.success({
                            title: 'Başarılı',
                            message: `${categoryUpdateAjaxModel.CategoryDto.Message}`,
                            position: 'topCenter'
                        });
                    } else {
                        let summaryText = '';
                        $('#validation-summary > ul > li').each(function () {
                            let text = $(this).text();
                            summaryText += `*${text}\n`;
                        });
                        iziToast.warning({
                            title: 'Başarısız!',
                            message: summaryText,
                            position: 'topCenter'
                        });
                    }
                }).fail(function (err) {
                    console.error(err); 
            });
        });

    });
});