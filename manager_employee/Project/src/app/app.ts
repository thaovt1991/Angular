import Swal from "sweetalert2"

export class App {

    static showDeleteConfirmDialog(t:string) {
        return Swal.fire({
            icon: 'warning',
            text: 'Bạn có chắc chắn muốn xóa "' + t +'" ?',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Xóa',
            cancelButtonText: 'Hủy',
        })
    }

    static showSuccessAlert(t:string) {
        Swal.fire({
            icon: 'success',
            title: t,
            // position: 'top-end',
            position: 'center',
            showConfirmButton: false,
            timer: 2000
        })
    }

    static showErrorAlert(t:string) {
        Swal.fire({
            icon: 'error',
            title: 'Warning',
            text: t,
            position: 'center',
        })
    }

    static showError403() {
        Swal.fire({
            icon: 'error',
            title: 'Access Denied',
            text: 'You are not authorized to perform this function!',
        })
    }
}