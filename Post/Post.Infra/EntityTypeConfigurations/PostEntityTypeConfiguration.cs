using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Post.Domain.Entities.CategoryAggregate;
using Post.Domain.Entities.PostAggregate;

namespace Post.Infra.EntityTypeConfigurations;

public class PostEntityTypeConfiguration : IEntityTypeConfiguration<Post.Domain.Entities.PostAggregate.Post>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.PostAggregate.Post> builder)
    {
        builder.HasOne<Category>()
            .WithMany()
            .HasForeignKey("CategoryId")
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasData(new Domain.Entities.PostAggregate.Post(
            id: 1,
            title: "بررسی ردمی نوت ۱۱ پرو 5G شیائومی",
            headline: "ردمی نوت ۱۱ پرو 5G در مقایسه‌با نسل قبل بهبود معناداری تجربه نمی‌کند و در بخش‌هایی همچون عملکرد و دوربین، رقبای سرسختی دارد. با بررسی ردمی نوت ۱۱ پرو همراه باشید.",
            description: "ردمی نوت ۱۱ پرو 5G در مقایسه‌با نسل قبل بهبود معناداری تجربه نمی‌کند و در بخش‌هایی همچون عملکرد و دوربین، رقبای سرسختی دارد. با بررسی ردمی نوت ۱۱ پرو همراه باشید.",
            keywords: "ردمی موبایل شیائومی",
            categoryId: 1
            ));
        builder.HasData(new Domain.Entities.PostAggregate.Post(
            id: 2,
            title: "احتمال رونمایی گلکسی اس ۲۳ در هفته دوم بهمن قوت گرفت",
            headline: "براساس شایعه‌ای جدید، سامسونگ قصد دارد اواسط بهمن مراسم Galaxy Unpacked را برای رونمایی گوشی‌های سری گلکسی اس ۲۳ برگزار کند.",
            description: "براساس شایعه‌ای جدید، سامسونگ قصد دارد اواسط بهمن مراسم Galaxy Unpacked را برای رونمایی گوشی‌های سری گلکسی اس ۲۳ برگزار کند.",
            keywords: "سامسونگ گلکسی رونمایی موبایل",
            categoryId: 1
            ));
        builder.HasData(new Domain.Entities.PostAggregate.Post(
            id: 3,
            title: "آنالیز ترکیب بدن گلکسی واچ 4 و گلکسی واچ 5 نسبتاً دقیق است",
            headline: "گلکسی واچ ۴ و گلکسی واچ ۵ هنگام انجام تجزیه‌و‌تحلیل امپدانس بیوالکتریک (BIA) به دقتی قابل مقایسه با معیارهای آزمایشگاهی اما کمتر از آن دست می‌یابد.",
            description: "گلکسی واچ ۴ و گلکسی واچ ۵ هنگام انجام تجزیه‌و‌تحلیل امپدانس بیوالکتریک (BIA) به دقتی قابل مقایسه با معیارهای آزمایشگاهی اما کمتر از آن دست می‌یابد.",
            keywords: "پوشیدنی گلکسی واچ",
            categoryId: 2
            ));
        builder.HasData(new Domain.Entities.PostAggregate.Post(
            id: 4,
            title: "قیمت هدفون Buds 4 و ساعت هوشمند Watch S2 شیائومی پیش از رونمایی اعلام شد",
            headline: "براساس گزارشی جدید، هدفون Buds 4 شیائومی در رویداد دهم آذر با قیمتی مقرون‌به‌صرفه‌تر از نسخه‌ی Buds 4 پرو فعلی، رونمایی خواهد شد.",
            description: "براساس گزارشی جدید، هدفون Buds 4 شیائومی در رویداد دهم آذر با قیمتی مقرون‌به‌صرفه‌تر از نسخه‌ی Buds 4 پرو فعلی، رونمایی خواهد شد.",
            keywords: "پوشیدنی شیائومی هدفون",
            categoryId: 2
            ));
    }
}
