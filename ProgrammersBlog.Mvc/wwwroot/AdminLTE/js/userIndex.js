﻿$(document).ready(function () {

    // DataTables start here.

    const dataTable = $('#usersTable').DataTable({
        dom:
            "<'row'<'col-sm-3'l><'col-sm-6 text-center'B><'col-sm-3'f>>" +
            "<'row'<'col-sm-12'tr>>" +
            "<'row'<'col-sm-5'i><'col-sm-7'p>>",
        buttons: [
            {
                text: 'Ekle',
                attr: {
                    id: "btnAdd",
                },
                className: 'btn btn-success mb-1',
                action: function (e, dt, node, config) {

                }
            },
            {
                text: 'Yenile',
                className: 'btn btn-warning mb-1',
                action: function (e, dt, node, config) {
                    $.ajax({
                        type: 'GET',
                        url: '/Admin/User/GetAllUsers/',
                        contentType: "application/json",
                        beforeSend: function () {
                            $('#usersTable').hide();
                            $('.spinner-border').show();
                        },
                        success: function (data) {
                            const userListDto = jQuery.parseJSON(data);
                            dataTable.clear();
                            console.log(userListDto);
                            if (userListDto.ResultStatus === 0) {
                                $.each(userListDto.Users.$values, function (index, users) {
                                    const newTableRow =dataTable.row.add([
                                        user.Id,
                                        user.UserName,
                                        user.Email,
                                        user.PhoneNumber,
                                        `<img src="/img/${user.Picture}" alt="${user.UserName}" class="my-image-table" />`,
                                        `
                                            <button class="btn btn-primary btn-sm btn-update" data-id="${user.Id}">
                                                <span class="fas fa-edit"></span>
                                            </button
                                            <button class="btn btn-danger btn-sm btn-delete" data-id="${user.Id}">
                                                <span class="fas fa-minus-circle"></span>
                                            </button>
                                        `
                                    ]).node();
                                    const jqueryTableRow = $(newTableRow);
                                    jqueryTableRow.attr('name', `${user.Id}`);
                                });
                                dataTable.draw();
                                $('.spinner-border').hide();
                                $('#usersTable').fadeIn(1400);
                            }
                            else {
                                toastr.error(`${userListDto.Message}`, 'İşlem Başarısız!');
                            }
                        },
                        error: function (err) {
                            console.log(err);
                            $('.spinner-border').hide();
                            $('#usersTable').fadeIn(1000);
                            toastr.error(`${err.responseText}`, 'HATA! İşlem Başarısız!');
                        }
                    })
                }
            }
        ],
        language: {
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
                    "1": "1 kayıt seçildi"
                },
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
                "fillHorizontal": "Hücreleri yatay olarak doldur",
                "fillVertical": "Hücreleri dikey olarak doldur",
                "fill": "Bütün hücreleri <i>%d<\/i> ile doldur",
                "info": "Detayı"
            },
            "buttons": {
                "collection": "Koleksiyon <span class=\"ui-button-icon-primary ui-icon ui-icon-triangle-1-s\"><\/span>",
                "colvis": "Sütun görünürlüğü",
                "colvisRestore": "Görünürlüğü eski haline getir",
                "copySuccess": {
                    "1": "1 satır panoya kopyalandı",
                    "_": "%ds satır panoya kopyalandı"
                },
                "copyTitle": "Panoya kopyala",
                "csv": "CSV",
                "excel": "Excel",
                "pageLength": {
                    "-1": "Bütün satırları göster",
                    "_": "%d satır göster",
                    "1": "1 Satır Göster"
                },
                "pdf": "PDF",
                "print": "Yazdır",
                "copy": "Kopyala",
                "copyKeys": "Tablodaki veriyi kopyalamak için CTRL veya u2318 + C tuşlarına basınız. İptal etmek için bu mesaja tıklayın veya escape tuşuna basın.",
                "createState": "Şuanki Görünümü Kaydet",
                "removeAllStates": "Tüm Görünümleri Sil",
                "removeState": "Aktif Görünümü Sil",
                "renameState": "Aktif Görünümün Adını Değiştir",
                "savedStates": "Kaydedilmiş Görünümler",
                "stateRestore": "Görünüm -&gt; %d",
                "updateState": "Aktif Görünümün Güncelle"
            },
            "searchBuilder": {
                "add": "Koşul Ekle",
                "button": {
                    "0": "Arama Oluşturucu",
                    "_": "Arama Oluşturucu (%d)"
                },
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
                        "startsWith": "İle başlar",
                        "notContains": "İçermeyen",
                        "notStartsWith": "Başlamayan",
                        "notEndsWith": "Bitmeyen"
                    },
                    "array": {
                        "contains": "İçerir",
                        "empty": "Boş",
                        "equals": "Eşittir",
                        "not": "Değildir",
                        "notEmpty": "Dolu",
                        "without": "Hariç"
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
                "value": "Değer",
                "clearAll": "Filtreleri Temizle"
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
                "title": "Etkin filtreler - %d",
                "showMessage": "Tümünü Göster",
                "collapseMessage": "Tümünü Gizle"
            },
            "thousands": ".",
            "datetime": {
                "amPm": [
                    "öö",
                    "ös"
                ],
                "hours": "Saat",
                "minutes": "Dakika",
                "next": "Sonraki",
                "previous": "Önceki",
                "seconds": "Saniye",
                "unknown": "Bilinmeyen",
                "weekdays": {
                    "6": "Paz",
                    "5": "Cmt",
                    "4": "Cum",
                    "3": "Per",
                    "2": "Çar",
                    "1": "Sal",
                    "0": "Pzt"
                },
                "months": {
                    "9": "Ekim",
                    "8": "Eylül",
                    "7": "Ağustos",
                    "6": "Temmuz",
                    "5": "Haziran",
                    "4": "Mayıs",
                    "3": "Nisan",
                    "2": "Mart",
                    "11": "Aralık",
                    "10": "Kasım",
                    "1": "Şubat",
                    "0": "Ocak"
                }
            },
            "decimal": ",",
            "editor": {
                "close": "Kapat",
                "create": {
                    "button": "Yeni",
                    "submit": "Kaydet",
                    "title": "Yeni kayıt oluştur"
                },
                "edit": {
                    "button": "Düzenle",
                    "submit": "Güncelle",
                    "title": "Kaydı düzenle"
                },
                "error": {
                    "system": "Bir sistem hatası oluştu (Ayrıntılı bilgi)"
                },
                "multi": {
                    "info": "Seçili kayıtlar bu alanda farklı değerler içeriyor. Seçili kayıtların hepsinde bu alana aynı değeri atamak için buraya tıklayın; aksi halde her kayıt bu alanda kendi değerini koruyacak.",
                    "noMulti": "Bu alan bir grup olarak değil ancak tekil olarak düzenlenebilir.",
                    "restore": "Değişiklikleri geri al",
                    "title": "Çoklu değer"
                },
                "remove": {
                    "button": "Sil",
                    "confirm": {
                        "_": "%d adet kaydı silmek istediğinize emin misiniz?",
                        "1": "Bu kaydı silmek istediğinizden emin misiniz?"
                    },
                    "submit": "Sil",
                    "title": "Kayıtları sil"
                }
            },
            "stateRestore": {
                "creationModal": {
                    "button": "Kaydet",
                    "columns": {
                        "search": "Kolon Araması",
                        "visible": "Kolon Görünümü"
                    },
                    "name": "Görünüm İsmi",
                    "order": "Sıralama",
                    "paging": "Sayfalama",
                    "scroller": "Kaydırma (Scrool)",
                    "search": "Arama",
                    "searchBuilder": "Arama Oluşturucu",
                    "select": "Seçimler",
                    "title": "Yeni Görünüm Oluştur",
                    "toggleLabel": "Kaydedilecek Olanlar"
                },
                "duplicateError": "Bu Görünüm Daha Önce Tanımlanmış",
                "emptyError": "Görünüm Boş Olamaz",
                "emptyStates": "Herhangi Bir Görünüm Yok",
                "removeJoiner": "ve",
                "removeSubmit": "Sil",
                "removeTitle": "Görünüm Sil",
                "renameButton": "Değiştir",
                "renameLabel": "Görünüme Yeni İsim Ver -&gt; %s:",
                "renameTitle": "Görünüm İsmini Değiştir",
                "removeConfirm": "Görünümü silmek istediğinize emin misiniz?",
                "removeError": "Görünüm silinemedi"
            },
            "emptyTable": "Tabloda veri bulunmuyor",
            "searchPlaceholder": "Arayın...",
            "infoPostFix": " "
        }
    });
    //  Data Table Ends Here

    // Ajax GET / Getting the _UserAddPartial as Modal Form starts from here.
    $(function () {
        const url = '/Admin/User/Add/';
        const placeHolderDiv = $('#modalPlaceHolder');
        $('#btnAdd').click(function () {
            $.get(url).done(function (data) {
                placeHolderDiv.html(data);
                placeHolderDiv.find(".modal").modal('show');
            });
        });

        // Ajax GET / Getting the _UserAddPartial as Modal Form ends here.

        // Ajax POST / Posting the FormData as UserAddDto starts from here. 

        placeHolderDiv.on('click', '#btnSave', function (event) {
            event.preventDefault();
            const form = $('#form-user-add');
            const actionUrl = form.attr('action');
            const dataToSend = new FormData(form.get(0));

            $.ajax({
                url: actionUrl, // Ajax çağırısının yapılacağı URL
                type: 'POST', // İstek türü
                data: dataToSend, // Gönderilecek veri
                processData: false, // Veri işlenmeyecek
                contentType: false, // İçerik türü belirtilmeyen
                success: function (data) {
                    console.log(data);
                    const userAddAjaxModel = jQuery.parseJSON(data);
                    console.log(userAddAjaxModel);
                    const newFormBody = $('.modal-body', userAddAjaxModel.UserAddPartial);
                    placeHolderDiv.find('.modal-body').replaceWith(newFormBody); // Eski form body yenisiyle değiştiriyor.
                    const isValid = newFormBody.find('[name="IsValid"]').val() === 'True'; // Formun geçerliliğinin kontrolü
                    if (isValid) {
                        placeHolderDiv.find('.modal').modal('hide');
                        const newTableRow = dataTable.row.add([
                            userAddAjaxModel.UserDto.User.Id,
                            userAddAjaxModel.UserDto.User.UserName,
                            userAddAjaxModel.UserDto.User.Email,
                            userAddAjaxModel.UserDto.User.PhoneNumber,
                            `<img src="/img/${userAddAjaxModel.UserDto.User.Picture}" alt="${userAddAjaxModel.UserDto.User.UserName}" class="my-image-table" />`,
                            `
                                  <button class="btn btn-primary btn-sm btn-update" data-id="${userAddAjaxModel.UserDto.User.Id}">
                                      <span class="fas fa-edit"></span>
                                  </button
                                  <button class="btn btn-danger btn-sm btn-delete" data-id="${userAddAjaxModel.UserDto.User.Id}">
                                       <span class="fas fa-minus-circle"></span>
                                  </button>
                            `
                        ]).node();
                        const jqueryTableRow = $(newTableRow);
                        jqueryTableRow.attr('name', `${userAddAjaxModel.UserDto.User.Id}`);
                        dataTable.row(newTableRow).draw();
                        toastr.success(`${userAddAjaxModel.UserDto.Message}`, 'İşlem Başarılı!');
                    }
                    else {
                        let summaryText = "";
                        $('#validation-summary > ul > li').each(function () {
                            let text = $(this).text();
                            summaryText = `*${text}\n`;
                        });
                        toastr.warning(summaryText);
                    }
                },
            error: function(err) {
               console.log(err);
                toastr.error(`${err.responseText}`, 'Hata!'); // Hata mesajı
            }
            });
        });
    });

    // Ajax POST / Posting the FormData as UserAddDto ends here.

    // Ajax POST / Deleting a User starts from here.

    $(document).on('click', '.btn-delete', function (event) {
        event.preventDefault();
        const id = $(this).attr('data-id');
        const tableRow = $(`[name="${id}"]`); // button'un click olduğu satırı döner.
        const userName = tableRow.find('td:eq(1)').text();
        Swal.fire({
            title: "Silmek istediğinize emin misiniz?",
            text: `${userName} adlı kullanıcı silinecektir. Bu işlem geri alınamaz!`,
            icon: "warning",
            showCancelButton: true,
            confirmButtonColor: "#3085d6",
            cancelButtonColor: "#d33",
            confirmButtonText: "Evet, silmek istiyorum!",
            cancelButtonText: "Hayır"
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    type: 'POST',
                    dataType: 'json',
                    data: { userId: id },
                    url: '/Admin/User/Delete/',
                    success: function (data) {
                        const userDto = jQuery.parseJSON(data);
                        if (categoryDto.ResultStatus === 0) {
                            Swal.fire({
                                title: "Silindi!",
                                text: `${userDto.User.UserName} adlı kullanıcı başarıyla silindi.`,
                                icon: "success"
                            });
                            dataTable.row(tableRow).remove().draw(); // DataTable API ile satır silme işlemi.
                        }
                        else {
                            Swal.fire({
                                icon: "error",
                                title: "Başarısız İşlem!",
                                text: `${userDto.Message}`,
                            });
                        }
                    },
                    error: function (err) {
                        console.log(err);
                        toastr.error(`${err.responseText}`, "HATA!");
                    }
                });
            }
        });
    });

    /* Ajax GET / Getting the _UserUpdatePartial as Modal Form starts from here. */

    $(function () {
        const url = '/Admin/User/Update/';
        const placeHolderDiv = $('#modalPlaceHolder');

        $(document).on('click', '.btn-update', function (event) {
            event.preventDefault();
            const id = $(this).attr('data-id');
            $.get(url, { userId: id }).done(function (data) {
                placeHolderDiv.html(data);
                placeHolderDiv.find('.modal').modal('show');
            }).fail(function () {
                toastr.error("Bir Hata Oluştu!");
            });
        });

        // Ajax POST / Updating a User starts from here

        placeHolderDiv.on('click', '#btnUpdate', function (event) {
            event.preventDefault();
            const form = $('#form-user-update');
            const actionUrl = form.attr('action');
            const dataToSend = new FormData(form.get(0));

            $.ajax({
                url: actionUrl, // Ajax çağırısının yapılacağı URL
                type: 'POST', // İstek türü
                data: dataToSend, // Gönderilecek veri
                processData: false, // Veri işlenmeyecek
                contentType: false, // İçerik türü belirtilmeyen
                success: function (data) {
                    const userUpdateAjaxModel = jQuery.parseJSON(data);
                    console.log(userUpdateAjaxModel);

                    if (userUpdateAjaxModel.UserDto !== null) {
                        const id = userUpdateAjaxModel.UserDto.User.Id;
                        const tableRow = $(`[name="${id}"]`);
                    }

                    const newFormBody = $('.modal-body', userUpdateAjaxModel.UserUpdatePartial);
                    placeHolderDiv.find('.modal-body').replaceWith(newFormBody);
                    const isValid = newFormBody.find('[name="IsValid"]').val() === 'True';

                    if (isValid) {
                        placeHolderDiv.find('.modal').modal('hide');

                        dataTable.row(tableRow).data([
                            userUpdateAjaxModel.UserDto.User.Id,
                            userUpdateAjaxModel.UserDto.User.UserName,
                            userUpdateAjaxModel.UserDto.User.Email,
                            userUpdateAjaxModel.UserDto.User.PhoneNumber,
                            `<img src="/img/${userUpdateAjaxModel.UserDto.User.Picture}" alt="${userUpdateAjaxModel.UserDto.User.UserName}" class="my-image-table" />`,
                            `
                                  <button class="btn btn-primary btn-sm btn-update" data-id="${userUpdateAjaxModel.UserDto.User.Id}">
                                      <span class="fas fa-edit"></span>
                                  </button
                                  <button class="btn btn-danger btn-sm btn-delete" data-id="${userUpdateAjaxModel.UserDto.User.Id}">
                                       <span class="fas fa-minus-circle"></span>
                                  </button>
                            `
                        ]);
                        tableRow.attr("name", `${id}`);
                        dataTable.row(tableRow).invalidate();
                        toastr.success(`${userUpdateAjaxModel.UserDto.Message}`, "Başarılı İşlem!");
                    }
                    else {
                        let summaryText = "";
                        $('#validation-summary > ul > li').each(function () {
                            let text = $(this).text();
                            summaryText = `*${text}\n`;
                        });
                        toastr.warning(summaryText);
                    }
                },
                error: function (error) {
                    console.log(error);
                    toastr.error(`${err.responseText}`, 'HATA! İşlem Başarısız!');
                }
            });
        });

        /* Ajax GET / Getting the _UserUpdatePartial as Modal Form ends from here. */

    });
});