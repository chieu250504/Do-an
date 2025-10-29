//truyện đề cử
document.addEventListener("DOMContentLoaded", function () {
    const topList = document.getElementById("topList");
    const items = Array.from(topList.children);
    const visibleCount = 5;
    const itemWidth = topList.children[0].offsetWidth + 18;
    let currentIndex = 0;
    let isAnimating = false;
    let autoSlideInterval;
    let position = -visibleCount * itemWidth;

    // Clone phần tử 2 đầu
    for (let i = 0; i < visibleCount; i++) {
        topList.appendChild(items[i].cloneNode(true));
        topList.insertBefore(items[items.length - 1 - i].cloneNode(true), topList.firstChild);
    }

    const totalItems = topList.children.length;
    topList.style.transform = `translateX(${position}px)`;
    topList.style.transition = "transform 0.4s ease-in-out";

    function moveCarousel(direction) {
        if (isAnimating) return;
        isAnimating = true;

        if (direction === "right") {
            currentIndex++;
            position -= itemWidth;
        } else {
            currentIndex--;
            position += itemWidth;
        }

        topList.style.transform = `translateX(${position}px)`;
    }

    // 👉 Chỉ add transitionend 1 lần duy nhất
    topList.addEventListener("transitionend", () => {
        if (currentIndex >= items.length) {
            currentIndex = 0;
            position = -visibleCount * itemWidth;
            topList.style.transition = "none";
            topList.style.transform = `translateX(${position}px)`;
            setTimeout(() => (topList.style.transition = "transform 0.4s ease-in-out"), 20);
        } else if (currentIndex < 0) {
            currentIndex = items.length - 1;
            position = -((visibleCount + items.length - 1) * itemWidth);
            topList.style.transition = "none";
            topList.style.transform = `translateX(${position}px)`;
            setTimeout(() => (topList.style.transition = "transform 0.4s ease-in-out"), 20);
        }
        isAnimating = false;
    });

    document.querySelector(".right-btn").addEventListener("click", () => moveCarousel("right"));
    document.querySelector(".left-btn").addEventListener("click", () => moveCarousel("left"));

    function startAutoSlide() {
        autoSlideInterval = setInterval(() => moveCarousel("right"), 4000);
    }
    function stopAutoSlide() {
        clearInterval(autoSlideInterval);
    }

    startAutoSlide();

    const carouselContainer = document.querySelector(".carousel-container");
    carouselContainer.addEventListener("mouseenter", stopAutoSlide);
    carouselContainer.addEventListener("mouseleave", startAutoSlide);
});

//BXH
document.querySelectorAll('input[name="rankingFilter"]').forEach(radio => {
    radio.addEventListener('change', e => {
        const value = e.target.value;

        document.querySelectorAll('.ranking-list').forEach(list => list.style.display = 'none');
        document.querySelector(`.ranking-${value}`).style.display = 'block';
    });
});
//đổi màu nền sáng- tối
document.addEventListener('DOMContentLoaded', (event) => {
    // 1. Lấy các phần tử cần thiết
    const toggleButton = document.getElementById('theme-toggle');
    const bodyElement = document.body;

    // 2. Kiểm tra trạng thái đã lưu (nếu có)
    const savedTheme = localStorage.getItem('theme');
    if (savedTheme === 'dark') {
        bodyElement.classList.add('dark-mode');
    }

    // 3. Xử lý sự kiện nhấn nút
    toggleButton.addEventListener('click', (e) => {
        // Ngăn hành vi mặc định của thẻ <a> (ngăn cuộn lên đầu trang)
        e.preventDefault();

        // Chuyển đổi class 'dark-mode' trên thẻ body
        bodyElement.classList.toggle('dark-mode');

        // 4. Lưu trạng thái mới vào Local Storage
        if (bodyElement.classList.contains('dark-mode')) {
            localStorage.setItem('theme', 'dark');
        } else {
            localStorage.setItem('theme', 'light');
        }
    });
});
// tim kiem
document.getElementById('searchButton').addEventListener('click', function () {
    const keyword = document.getElementById('searchInput').value.trim();
    if (keyword) {
        window.location.href = '/Search?keyword=' + encodeURIComponent(keyword);
    }
});

