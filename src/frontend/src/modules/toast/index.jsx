export default class Toast {
    constructor(text, color, ms) {
        if (Toast.instances.length >= 5) { // Ограничение до
            return
        }

        this.text = text
        this.color = color
        this.ms = ms

        Toast.instances.push(this)

        let that = this
        this.index = Toast.instances.findIndex(function(el) {
            if (el === that) {
                return true
            }

            return false
        })

        this.draw()
    }

    static Initialize(element, parent) {
        element.classList.add('toast-box')
    
        parent.appendChild(element)

        window.ToastBox = element
    }

    draw() {
        this.toast = document.createElement('div')

        this.toast.innerHTML = `<span class="toast-icon"></span>${this.text}`
        this.toast.classList.add('toast-message')

        this.toast.style.background = this.color
        
        this.toast.id = `toast-message-${this.index}`
      
        window.ToastBox.appendChild(this.toast)

        let that = this
        setTimeout(() => that.remove(), this.ms)
    }

    remove() {
        let that = this
        let index = Toast.instances.findIndex(function(el) {
            if (el === that) {
                return true
            }

            return false
        })

        let elem = document.getElementById(`toast-message-${this.index}`)

        if (elem) {
            elem.parentNode.removeChild(elem)
        }

        Toast.instances.splice(index, 1)
    }
}

Toast.instances = []